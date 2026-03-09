@echo off

PowerShell.exe -Command "dotnet ef dbcontext scaffold 'Host=31.97.169.107;Port=5432;Database=db_identity;Username=HCUser_qa;Password=rT8$M!Z2@9xK#AqW7eLp;' Npgsql.EntityFrameworkCore.PostgreSQL -o EntityFramework/Tables -c EntityFrameworkContext --force"

pause
goto begin
