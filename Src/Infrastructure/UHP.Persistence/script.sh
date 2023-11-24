#!/usr/bin/env bash
dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=uhp;Username=postgres;Password=123456" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -f 