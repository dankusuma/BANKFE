namespace BANKFE.Models
{
    public interface IModal
    {
        string Title { get; set; }
        string Desc { get; set; }
        string cssStyle { get; }
        string callback { get; set; }

        string GetMessage();
    }
}