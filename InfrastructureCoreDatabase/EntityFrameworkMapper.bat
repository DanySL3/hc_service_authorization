@echo off

PowerShell.exe -Command "dotnet ef dbcontext scaffold 'Host=192.168.1.95;Port=5432;Database=db_identity;Username=HCUser_PS;Password=hc+2024;' Npgsql.EntityFrameworkCore.PostgreSQL -o EntityFramework/Tables -c EntityFrameworkContext --force"

pause
goto begin
