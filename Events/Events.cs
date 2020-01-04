// Events.cs
// Designing types to expose and consume events.

using System;
using System.Threading;

// Step #1: define a type that holds any additional information
// that must be sent to consumers of event notification

internal class NewMailEventArgs : EventArgs
{
    private readonly String m_from;
    private readonly String m_to;
    private readonly String m_subject;

    public NewMailEventArgs(
        String from,
        String to,
        String subject
    )
    {
        m_from = from;
        m_to = to;
        m_subject = subject;
    }

    public String From { get { return m_from; } }
    public String To { get { return m_to; } }
    public String Subject { get { return m_subject; } }
}

internal class MailManager
{
    // Step #2: define the event member
    // event is public so that it is visible to consumers
    // the name of the member is NewMail
    // the type of the event member is EventHandler<NewMailEventArgs>
    //  which implies that all consumers must provide a callback
    //  method which matches this delegate type
    public event EventHandler<NewMailEventArgs> NewMail;

    // Step #3: define a method responsible for raising the event
    protected virtual void OnNewMail(NewMailEventArgs e)
    {
        // copy ref to delegate field into temporary for thread safety
        EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);

        // only attempt notification if some consumers have registered interest
        if (temp != null)
        {
            temp(this, e);
        }
    }

    // Step #4: define a method that translates input into raising of event
    public void SimulateNewMail(
        String from,
        String to,
        String subject
    )
    {
        NewMailEventArgs e = new NewMailEventArgs(from, to, subject);

        OnNewMail(e);
    }
}

// define a type that consumes the event
internal sealed class Fax
{
    public Fax(MailManager mm)
    {
        mm.NewMail += FaxMsg;
    }

    // invoked by MailManager when event is raised
    private void FaxMsg(Object sender, NewMailEventArgs e)
    {
        Console.WriteLine("Faxing mail message:");
        Console.WriteLine(" From = {0}, To = {1}, Subject = {2}",
            e.From, e.To, e.Subject);
    }

    // may be invoked to unregister the Fax instance
    // from further notifications of the event
    public void Unregister(MailManager mm)
    {
        mm.NewMail -= FaxMsg;
    }
}

// define a type that consumes an event
internal class Pager
{
    public Pager(MailManager mm)
    {
        mm.NewMail += PagerMsg;
    }

    // invoked by MailManager when event is raised
    private void PagerMsg(Object sender, NewMailEventArgs e)
    {
        Console.WriteLine("Displaying pager mail message:");
        Console.WriteLine(" From = {0}, To = {1}, Subject = {2}",
            e.From, e.To, e.Subject);
    }

    // may be invoked to unregister the Pager instance
    // from further notifications of the event
    public void Unregister(MailManager mm)
    {
        mm.NewMail -= PagerMsg;
    }
}

public sealed class Program
{
    public static void Main()
    {
        MailManager mm = new MailManager();

        Fax   f = new Fax(mm);
        Pager p = new Pager(mm);

        Console.WriteLine("Simulating new mail...");

        mm.SimulateNewMail("Jyn", "Rebels", "Death Star Plans");

        Console.WriteLine("Unregistering consumers...");
        f.Unregister(mm);
        p.Unregister(mm);

        Console.WriteLine("Simulating new mail...");

        mm.SimulateNewMail("Kylo", "Galaxy", "Moody Nonsensical Babbling");
    }
}