using HRMData.Application.Interfaces;
using HRMData.Infrastructure.OptionsModel;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace HRMData.Infrastructure.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSettings _emailSettings;

        public EmailServices(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendResetPasswordEmailAsync(string resetPasswordEmailLink, string toEmail)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "LocalHost | Şifre Sıfırlama Linki";
            mailMessage.Body = @$"<html>
<head>
	<meta http-equiv=""content-type"" content=""text/html; charset=utf-8"">
  	<meta name=""viewport"" content=""width=device-width, initial-scale=1.0;"">
 	<meta name=""format-detection"" content=""telephone=no""/>	

	<style>
/* Reset styles */ 
body {{ margin: 0; padding: 0; min-width: 100%; width: 100% !important; height: 100% !important;}}
body, table, td, div, p, a {{ -webkit-font-smoothing: antialiased; text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; line-height: 100%; }}
table, td {{ mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse !important; border-spacing: 0; }}
img {{ border: 0; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }}
#outlook a {{ padding: 0; }}
.ReadMsgBody {{ width: 100%; }} .ExternalClass {{ width: 100%; }}
.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {{ line-height: 100%; }}

/* Rounded corners for advanced mail clients only */ 
@media all and (min-width: 560px) {{
	.container {{ border-radius: 8px; -webkit-border-radius: 8px; -moz-border-radius: 8px; -khtml-border-radius: 8px;}}
}}

/* Set color for auto links (addresses, dates, etc.) */ 
a, a:hover {{
	color: #127DB3;
}}
.footer a, .footer a:hover {{
	color: #999999;
}}

 	</style>

	<!-- MESSAGE SUBJECT -->
	<title>Naeron - Şifre Sıfırlama</title>

</head>

<!-- BODY -->
<!-- Set message background color (twice) and text color (twice) -->
<body topmargin=""0"" rightmargin=""0"" bottommargin=""0"" leftmargin=""0"" marginwidth=""0"" marginheight=""0"" width=""100%"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; width: 100%; height: 100%; -webkit-font-smoothing: antialiased; text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; line-height: 100%;
	background-color: #F0F0F0;
	color: #000000;""
	bgcolor=""#F0F0F0""
	text=""#000000"">

<!-- SECTION / BACKGROUND -->
<!-- Set message background color one again -->
<table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; width: 100%;"" class=""background""><tr><td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0;""
	bgcolor=""#F0F0F0"">

<!-- WRAPPER -->
<!-- Set wrapper width (twice) -->
<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""
	width=""560"" style=""border-collapse: collapse; border-spacing: 0; padding: 0; width: inherit;
	max-width: 560px;"" class=""wrapper"">

	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 20px;
			padding-bottom: 20px;"">

			<div style=""display: none; visibility: hidden; overflow: hidden; opacity: 0; font-size: 1px; line-height: 1px; height: 0; max-height: 0; max-width: 0;
			color: #F0F0F0;"" class=""preheader"">
				HRMData şifre sıfırlama talebiniz.</div>			
		</td>
	</tr>

<!-- End of WRAPPER -->
</table>

<!-- WRAPPER / CONTEINER -->
<!-- Set conteiner background color -->
<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""
	bgcolor=""#FFFFFF""
	width=""560"" style=""border-collapse: collapse; border-spacing: 0; padding: 0; width: inherit;
	max-width: 560px;"" class=""container"">
	<!-- SUBHEADER -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 24px; font-weight: bold; line-height: 130%;
			padding-top: 25px;
			color: #000000;
			font-family: sans-serif;"" class=""header"">
				HRMData Şifre Sıfırlama
		</td>
	</tr>
	<!-- PARAGRAPH -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 17px; font-weight: 400; line-height: 160%;
			padding-top: 25px; 
			color: #000000;
			font-family: sans-serif;"" class=""paragraph"">
				Bu e-posta HRMData uygulamasından şifre sıfırlama talebi yapıldığı için gönderilmiştir. Sıfırlama işlemini yapmak için ""Şifremi Sıfırla"" düğmesine basın.
		</td>
	</tr>

	<!-- BUTTON -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 25px;
			padding-bottom: 5px;"" class=""button""><a
			href={resetPasswordEmailLink} target=""_blank"" style=""text-decoration: underline;"">
				<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" style=""max-width: 240px; min-width: 120px; border-collapse: collapse; border-spacing: 0; padding: 0;""><tr><td align=""center"" valign=""middle"" style=""padding: 12px 24px; margin: 0; text-decoration: underline; border-collapse: collapse; border-spacing: 0; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; -khtml-border-radius: 4px;""
					bgcolor=""#E9703E""><a target=""_blank"" style=""text-decoration: underline;
					color: #FFFFFF; font-family: sans-serif; font-size: 17px; font-weight: 400; line-height: 120%;""
					href={resetPasswordEmailLink}>
						ŞİFREMİ SIFIRLA
					</a>
			</td></tr>
<!-- PARAGRAPH -->

			</table></a>
		</td>
	</tr>
		<!-- LINE -->
	<!-- Set line color -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 25px;"" class=""line""><hr
			color=""#E0E0E0"" align=""center"" width=""100%"" size=""1"" noshade style=""margin: 0; padding: 0;"" />
		</td>
	</tr>
		<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 14px; font-weight: 400; line-height: 160%;
			padding-top: 25px; 
			color: #ff0000;
			font-family: sans-serif;"" class=""paragraph"">
			Talebi siz yapmadıysanız bu e-postayı dikkate almayın. </br> Endişelenmeyin! Şifreniz ele geçirilmedi veya sıfırlanmadı.
		</td>
	</tr>
	<!-- LINE -->
	<!-- Set line color -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 25px;"" class=""line""><hr
			color=""#E0E0E0"" align=""center"" width=""100%"" size=""1"" noshade style=""margin: 0; padding: 0;"" />
		</td>
	</tr>

	<!-- PARAGRAPH -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 17px; font-weight: 400; line-height: 160%;
			padding-top: 20px;
			padding-bottom: 25px;
			color: #000000;
			font-family: sans-serif;"" class=""paragraph"">
				Soru ve görüşleriniz için lütfen iletişime geçin <a href=""mailto:info.hrmdata@gmail.com"" target=""_blank"" style=""color: #127DB3; font-family: sans-serif; font-size: 17px; font-weight: 400; line-height: 160%;"">info.hrmdata@gmail.com</a>
		</td>
	</tr>

<!-- End of WRAPPER -->
</table>

	<!-- FOOTER -->
<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""
	width=""560"" style=""border-collapse: collapse; border-spacing: 0; padding: 0; width: inherit;
	max-width: 560px;"" class=""wrapper"">

	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 25px;"" class=""social-icons"">

		</td>
	</tr>
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 13px; font-weight: 400; line-height: 150%;
			padding-top: 20px;
			padding-bottom: 20px;
			color: #999999;
			font-family: sans-serif;"" class=""footer"">
				Bu e-posta HRMData uygulamasından şifre sıfırlama talebi yapıldığı için gönderilmiştir. </br> Talebi siz yapmadıysanız lütfen bu e-postayı dikkate almayın.
		</td>
	</tr>
</table>
</td></tr>
</table>
</body>
</html>";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);

        }
        public async Task SendPasswordChangeEmailAsync(string passwordChangeEmailLink, string toEmail, string Password)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "HRMData | Şifre Değiştirme Linki";
            mailMessage.Body = @$"<html>
<head>
	<meta http-equiv=""content-type"" content=""text/html; charset=utf-8"">
  	<meta name=""viewport"" content=""width=device-width, initial-scale=1.0;"">
 	<meta name=""format-detection"" content=""telephone=no""/>	

	<style>
/* Reset styles */ 
body {{ margin: 0; padding: 0; min-width: 100%; width: 100% !important; height: 100% !important;}}
body, table, td, div, p, a {{ -webkit-font-smoothing: antialiased; text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; line-height: 100%; }}
table, td {{ mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse !important; border-spacing: 0; }}
img {{ border: 0; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }}
#outlook a {{ padding: 0; }}
.ReadMsgBody {{ width: 100%; }} .ExternalClass {{ width: 100%; }}
.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {{ line-height: 100%; }}

/* Rounded corners for advanced mail clients only */ 
@media all and (min-width: 560px) {{
	.container {{ border-radius: 8px; -webkit-border-radius: 8px; -moz-border-radius: 8px; -khtml-border-radius: 8px;}}
}}

/* Set color for auto links (addresses, dates, etc.) */ 
a, a:hover {{
	color: #127DB3;
}}
.footer a, .footer a:hover {{
	color: #999999;
}}

 	</style>

	<!-- MESSAGE SUBJECT -->
	<title>Naeron - Şifre Sıfırlama</title>

</head>

<!-- BODY -->
<!-- Set message background color (twice) and text color (twice) -->
<body topmargin=""0"" rightmargin=""0"" bottommargin=""0"" leftmargin=""0"" marginwidth=""0"" marginheight=""0"" width=""100%"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; width: 100%; height: 100%; -webkit-font-smoothing: antialiased; text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; line-height: 100%;
	background-color: #F0F0F0;
	color: #000000;""
	bgcolor=""#F0F0F0""
	text=""#000000"">

<!-- SECTION / BACKGROUND -->
<!-- Set message background color one again -->
<table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; width: 100%;"" class=""background""><tr><td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0;""
	bgcolor=""#F0F0F0"">

<!-- WRAPPER -->
<!-- Set wrapper width (twice) -->
<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""
	width=""560"" style=""border-collapse: collapse; border-spacing: 0; padding: 0; width: inherit;
	max-width: 560px;"" class=""wrapper"">

	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 20px;
			padding-bottom: 20px;"">

			<div style=""display: none; visibility: hidden; overflow: hidden; opacity: 0; font-size: 1px; line-height: 1px; height: 0; max-height: 0; max-width: 0;
			color: #F0F0F0;"" class=""preheader"">
				HRMData şifre sıfırlama talebiniz.</div>			
		</td>
	</tr>

<!-- End of WRAPPER -->
</table>

<!-- WRAPPER / CONTEINER -->
<!-- Set conteiner background color -->
<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""
	bgcolor=""#FFFFFF""
	width=""560"" style=""border-collapse: collapse; border-spacing: 0; padding: 0; width: inherit;
	max-width: 560px;"" class=""container"">
	<!-- SUBHEADER -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 24px; font-weight: bold; line-height: 130%;
			padding-top: 25px;
			color: #000000;
			font-family: sans-serif;"" class=""header"">
				HRMData Şifre Sıfırlama
		</td>
	</tr>
	<!-- PARAGRAPH -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 17px; font-weight: 400; line-height: 160%;
			padding-top: 25px; 
			color: #000000;
			font-family: sans-serif;"" class=""paragraph"">
				Bu e-posta HRMData uygulamasına başarılı bir şekilde kaydınız yapıldığı için gönderilmiştir. Şifrenizi değiştirmek için ""Şifremi Değiştir"" düğmesine basın veya sisteme giriş yaptıktan sonra şifrenizi değiştirin.
<br>
<br>
Email : {toEmail}
<br>
Geçici Şifre : {Password}
<br>
		</td>
	</tr>

	<!-- BUTTON -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 25px;
			padding-bottom: 5px;"" class=""button""><a
			href={passwordChangeEmailLink} target=""_blank"" style=""text-decoration: underline;"">
				<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" style=""max-width: 240px; min-width: 120px; border-collapse: collapse; border-spacing: 0; padding: 0;""><tr><td align=""center"" valign=""middle"" style=""padding: 12px 24px; margin: 0; text-decoration: underline; border-collapse: collapse; border-spacing: 0; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; -khtml-border-radius: 4px;""
					bgcolor=""#E9703E""><a target=""_blank"" style=""text-decoration: underline;
					color: #FFFFFF; font-family: sans-serif; font-size: 17px; font-weight: 400; line-height: 120%;""
					href={passwordChangeEmailLink}>
						ŞİFREMİ DEĞİŞTİR
					</a>
			</td></tr>
<!-- PARAGRAPH -->

			</table></a>
		</td>
	</tr>
		<!-- LINE -->
	<!-- Set line color -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 25px;"" class=""line""><hr
			color=""#E0E0E0"" align=""center"" width=""100%"" size=""1"" noshade style=""margin: 0; padding: 0;"" />
		</td>
	</tr>		
	<!-- LINE -->
	<!-- Set line color -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 25px;"" class=""line""><hr
			color=""#E0E0E0"" align=""center"" width=""100%"" size=""1"" noshade style=""margin: 0; padding: 0;"" />
		</td>
	</tr>

	<!-- PARAGRAPH -->
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 17px; font-weight: 400; line-height: 160%;
			padding-top: 20px;
			padding-bottom: 25px;
			color: #000000;
			font-family: sans-serif;"" class=""paragraph"">
				Soru ve görüşleriniz için lütfen iletişime geçin <a href=""mailto:info.hrmdata@gmail.com"" target=""_blank"" style=""color: #127DB3; font-family: sans-serif; font-size: 17px; font-weight: 400; line-height: 160%;"">info.hrmdata@gmail.com</a>
		</td>
	</tr>

<!-- End of WRAPPER -->
</table>

	<!-- FOOTER -->
<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""
	width=""560"" style=""border-collapse: collapse; border-spacing: 0; padding: 0; width: inherit;
	max-width: 560px;"" class=""wrapper"">

	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%;
			padding-top: 25px;"" class=""social-icons"">

		</td>
	</tr>
	<tr>
		<td align=""center"" valign=""top"" style=""border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; padding-left: 6.25%; padding-right: 6.25%; width: 87.5%; font-size: 13px; font-weight: 400; line-height: 150%;
			padding-top: 20px;
			padding-bottom: 20px;
			color: #999999;
			font-family: sans-serif;"" class=""footer"">
				Bu e-posta HRMData uygulamasına kaydınızın başarılı bir şekilde yapıldığı için gönderilmiştir.
	</tr>
</table>
</td></tr>
</table>
</body>
</html>";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);

        }
    }

}

