namespace MySpot.Api.Exceptions;

public class InvalidLicensePlateException : CustomException
{
    public InvalidLicensePlateException() : base("License plate is invalid.")
    {
    }
}