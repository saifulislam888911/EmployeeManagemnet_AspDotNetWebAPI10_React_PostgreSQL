namespace Domain.Exceptions;

public class DuplicateNidException : Exception
{
    public DuplicateNidException(string nid) : base($"An employee with NID '{nid}' already exists.")
    {
    }
}
