namespace PulseHub.SharedKernel.Templates;

public static class EmailTemplates
{
    public static string GetVerificationEmailBodyHtml(string username,string emailCode)
    {
        return $@"
                 <html>
                    <body>
                        <p>Hello: '{username}',</p>
                        <p>Please use the following verification code to complete your registration:</p>
                        <h2>{emailCode}</h2>
                        <p>If you did not request this verification, please ignore this email.</p>
                        <p>Thank you!</p>
                    </body>
                 </html>";
    }
}
