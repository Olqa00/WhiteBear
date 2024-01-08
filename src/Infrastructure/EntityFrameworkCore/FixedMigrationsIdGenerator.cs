namespace WhiteBear.Infrastructure.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Migrations;

public sealed class FixedMigrationsIdGenerator : IMigrationsIdGenerator
{
    public string GenerateId(string name)
    {
        return name;
    }
 
    public string GetName(string id)
    {
        return id;
    }
 
    public bool IsValidId(string value)
    {
        return true;
    }
}
