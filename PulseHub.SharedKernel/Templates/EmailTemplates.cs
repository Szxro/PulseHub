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

    public static string GetWelcomeEmailBodyHtml(string userName)
    {
        return $@"
                    <html>
                        <body>
                            <p>Hi: {userName},</p>
                            <p>Welcome to PulseHub! We are excited to have you with us.</p>
                            <p>If you have any questions, feel free to contact our support team.</p>
                            <p>Best Regards,<br>The PulseHub Team.</p>
                        </body>
                    </html>";
    }
}
