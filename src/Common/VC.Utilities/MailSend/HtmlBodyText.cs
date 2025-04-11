namespace VC.Utilities.MailSend;

internal static class HtmlBodyText
{
    public static string GetBodyText(string header, string text)
    {
        return @$"
                <html lang=""ru"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                </head>
                <body style=""font-family: Arial, sans-serif; background-color: #f4f4f9; margin: 0; padding: 0; text-align: center; color: #333;"">
                    <div style=""max-width: 600px; margin: 20px auto; background: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); overflow: hidden; padding: 20px;"">
                        <div style=""background-color: #4CAF50; color: white; padding: 20px; border-radius: 8px 8px 0 0;"">
                            <h1 style=""text-align: center;"">{header}</h1>
                        </div>
                        <div style=""padding: 20px;"">
                            <p>Здравствуйте!</p>
                            <p>{text}</p>
                            <p>С уважением,<br>Команда поддержки</p>
                        </div>
                        <div style=""padding: 10px; font-size: 12px; color: #777;"">
                            <p>© {DateTime.UtcNow.Year} Наш ресурс. Все права защищены.</p>
                        </div>
                    </div>
                </body>
                </html>";
    }
}