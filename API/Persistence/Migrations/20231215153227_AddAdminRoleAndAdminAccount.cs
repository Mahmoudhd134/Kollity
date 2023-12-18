using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminRoleAndAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql(@"
USE [my_college]
GO
INSERT [dbo].[Roles] ([id], [name], [normalized_name], [concurrency_stamp]) VALUES (N'f638418a-05ed-4fe7-ef09-08dbfd848c38', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[Users] ([id], [type], [full_name_in_arabic], [user_name], [normalized_user_name], [email], [normalized_email], [email_confirmed], [password_hash], [security_stamp], [concurrency_stamp], [phone_number], [phone_number_confirmed], [two_factor_enabled], [lockout_end], [lockout_enabled], [access_failed_count]) VALUES (N'a2d1dae7-d933-469d-f3fb-08dbfd848bed', N'Doctor', NULL, N'Mahmoudhd134', N'MAHMOUDHD134', N'nassermahmoud571@gmail.com', N'NASSERMAHMOUD571@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEPRFyxksWTOaY3gzYwnqUGS8FT0q1kCjlaUo1KP/Uu3R1seoxDWoi1tlyw8Uc69YNA==', N'6TPMB3KY7R4NAIGXTMKLOWGRE2HQOOBY', N'a443bf96-da75-4046-8452-7d64553b4533', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[UserRoles] ([user_id], [role_id]) VALUES (N'a2d1dae7-d933-469d-f3fb-08dbfd848bed', N'f638418a-05ed-4fe7-ef09-08dbfd848c38')
GO
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
