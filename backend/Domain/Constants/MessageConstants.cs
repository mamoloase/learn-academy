namespace Domain.Constants;
public class MessageConstants
{
    public const string ExceptionNotFound = "can not found {0}";
    public const string ExceptionInvalidPhoneNumber = "invalid phone number";
    public const string ExceptionPhoneAlreadyExists = "user with phone already exists";
    public const string ExceptionEmailAlreadyExists = "user with email already exists";

    public const string ExceptionPasswordEmpty = "password can not empty";
    public const string ExceptionPasswordLength = "password lenght required 8";
    public const string ExceptionPasswordUppercaseLetter = "password required upper case charachter";
    public const string ExceptionPasswordLowercaseLetter = "password required lower case charachter";
    public const string ExceptionPasswordDigit = "password required one number";
    public const string ExceptionPasswordSpecialCharacter = "password required Special Character";

}
