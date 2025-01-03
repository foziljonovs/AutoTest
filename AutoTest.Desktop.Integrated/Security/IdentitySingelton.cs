namespace AutoTest.Desktop.Integrated.Security;

public class IdentitySingelton
{
    public static IdentitySingelton identitySingelton;

    public static IdentitySingelton GetInstance()
    {
        if (identitySingelton == null)
        {
            identitySingelton = new IdentitySingelton();
        }

        return identitySingelton;
    }

    public string Token { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;

    public void Reset()
    {
        Token = string.Empty;
        Id = 0;
        Firstname = string.Empty;
        Lastname = string.Empty;
        PhoneNumber = string.Empty;
        RoleName = string.Empty;
    }
}
