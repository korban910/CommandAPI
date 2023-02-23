using CommandAPI.Models;

namespace CommandAPI.Data;

public class SqlCommandAPIRepo : ICommandAPIRepo
{
    private readonly CommandContext _context;
    public SqlCommandAPIRepo(CommandContext context) => _context = context;
    
    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
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
        if (cmd == null)
        {
            throw new ArgumentNullException(nameof(cmd));
        }

        _context.CommandItems.Add(cmd);
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