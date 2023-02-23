using CommandAPI.Models;

namespace CommandAPI.Data;

public class SqlCommandAPIRepo : ICommandAPIRepo
{
    private readonly CommandContext _context;
    public SqlCommandAPIRepo(CommandContext context) => _context = context;
    
    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands()
    {
        if (_context.CommandItems != null) return _context.CommandItems.ToList();
        return null;
    }

    public Command GetCommandById(int id)
    {
        if (_context.CommandItems != null) return _context.CommandItems.FirstOrDefault(p => p.Id == id);
        return null;
    }

    public void CreateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new NotImplementedException();
    }
}