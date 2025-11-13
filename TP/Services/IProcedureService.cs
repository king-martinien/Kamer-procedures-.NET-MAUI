using TP.Models;

namespace TP.Services;

public interface IProcedureService
{
    Procedure? GetProcedureById(string id);
    List<Procedure> GetAllProcedures();
    List<Procedure> GetProceduresByCategory(string categoryId);
    List<Procedure> GetPopularProcedures();
}

