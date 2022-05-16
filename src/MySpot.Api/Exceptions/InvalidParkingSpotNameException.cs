namespace MySpot.Api.Exceptions;

public class InvalidParkingSpotNameException : CustomException
{
    public InvalidParkingSpotNameException(string name) 
        : base($"Parking spot name: '{name}' is invalid.")
    {
    }
}