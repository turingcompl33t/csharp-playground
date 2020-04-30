// clr_host.cpp
//
// Minimal application capable of hosting the CLR.
//
// Build
//	cl /EHsc /nologo /std:c++17 /W4 clr_host.cpp

#include <windows.h>
#include <metahost.h>
#include <wrl.h>

#include <iostream>

#pragma comment(lib, "mscoree.lib")  

using namespace Microsoft::WRL;

struct com_exception
{
	HRESULT result;

	com_exception(HRESULT result_)
		: result{ result_ }
	{}
};

void check(HRESULT result)
{
	if (S_OK != result)
	{
		throw com_exception{ result };
	}
}

int main()
{
	check(::CoInitialize(nullptr));

	std::cout << "Hello CLR\n";

	auto metahost = ComPtr<ICLRMetaHost>{};
	
	// acquire a pointer to the ICLRMetaHost interface
	check(::CLRCreateInstance(
		CLSID_CLRMetaHost, 
		IID_ICLRMetaHost, 
		reinterpret_cast<void**>(metahost.GetAddressOf())));

	auto runtime_info = ComPtr<ICLRRuntimeInfo>{};

	// load the specified version of the runtime
	check(metahost->GetRuntime(
		L"v4.0.30319", 
		IID_ICLRRuntimeInfo, 
		reinterpret_cast<void**>(runtime_info.GetAddressOf())));

	// check the current status of the runtime
		
	auto is_loaded = BOOL{};
	auto is_started = BOOL{};
	auto start_flags = unsigned long{};

	check(runtime_info->IsLoaded(::GetCurrentProcess(), &is_loaded));
	check(runtime_info->IsStarted(&is_started, &start_flags));

	std::cout << "Runtime loaded: " << is_loaded << '\n'
		<< "Runtime started: " << is_started << '\n';

	auto runtime = ComPtr<ICLRRuntimeHost>{};

	// load and start the runtime

	check(runtime_info->GetInterface(
		CLSID_CLRRuntimeHost, 
		IID_ICLRRuntimeHost, 
		reinterpret_cast<void**>(runtime.GetAddressOf())));

	check(runtime->Start());

	// now check the runtime's status again

	check(runtime_info->IsLoaded(::GetCurrentProcess(), &is_loaded));
	check(runtime_info->IsStarted(&is_started, &start_flags));

	std::cout << "Runtime loaded: " << is_loaded << '\n'
		<< "Runtime started: " << is_started << '\n';

	// invoke a method in an assembly

	auto retval = unsigned long{};

	check(runtime->ExecuteInDefaultAppDomain(
		L"LoadMe.dll",
		L"LoadMe",
		L"InvokeMe",
		L"Data From Host App",
		&retval));

	std::cout << "LoadMe::InvokeMe() returned " << retval << '\n';

	std::cin.get();
	::CoUninitialize();
}