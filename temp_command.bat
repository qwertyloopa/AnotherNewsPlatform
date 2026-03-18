@echo off
chcp 65001 >nul
cd /d "d:\vs projects\AnotherNewsPlatform"
dotnet ef migrations add SourceEntityExtended --project AnotherNewsPlatform.DataAccess