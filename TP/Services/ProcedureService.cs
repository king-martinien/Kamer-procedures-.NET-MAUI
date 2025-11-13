using TP.Models;

namespace TP.Services;

public class ProcedureService : IProcedureService
{
    private readonly List<Procedure> _procedures;

    public ProcedureService()
    {
        _procedures = InitializeProcedures();
    }

    public Procedure? GetProcedureById(string id)
    {
        return _procedures.FirstOrDefault(p => p.Id == id);
    }

    public List<Procedure> GetAllProcedures()
    {
        return _procedures.ToList();
    }

    public List<Procedure> GetProceduresByCategory(string categoryId)
    {
        return _procedures.Where(p => p.CategoryId == categoryId).ToList();
    }

    public List<Procedure> GetPopularProcedures()
    {
        return _procedures.Where(p => p.IsPopular).ToList();
    }

    private List<Procedure> InitializeProcedures()
    {
        return new List<Procedure>
        {
            // État Civil
            new Procedure
            {
                Id = "1",
                Title = "Carte Nationale d'Identité (CNI)",
                Description = "Procédure pour obtenir votre carte nationale d'identité Camerounaise.",
                Icon = "📄",
                Duration = "2-3 semaines",
                Cost = "15000 FCFA",
                Difficulty = "FACILE",
                StepCount = 5,
                CategoryId = "1",
                IsPopular = true
            },
            new Procedure
            {
                Id = "2",
                Title = "Passeport Biométrique",
                Description = "Procédure pour obtenir ou renouveler votre passeport biométrique Camerounais.",
                Icon = "🛂",
                Duration = "3-4 semaines",
                Cost = "75000 FCFA",
                Difficulty = "MOYEN",
                StepCount = 6,
                CategoryId = "1",
                IsPopular = true
            },
            new Procedure
            {
                Id = "3",
                Title = "Acte de Naissance",
                Description = "Procédure pour obtenir un acte de naissance au Cameroun.",
                Icon = "📋",
                Duration = "1-2 semaines",
                Cost = "5000 FCFA",
                Difficulty = "FACILE",
                StepCount = 4,
                CategoryId = "1"
            },
            new Procedure
            {
                Id = "4",
                Title = "Permis de Conduire",
                Description = "Procédure pour obtenir ou renouveler votre permis de conduire au Cameroun.",
                Icon = "🚗",
                Duration = "2-3 semaines",
                Cost = "25000 FCFA",
                Difficulty = "MOYEN",
                StepCount = 5,
                CategoryId = "5"
            },
            // Entreprises
            new Procedure
            {
                Id = "5",
                Title = "Création d'Entreprise",
                Description = "Créer une entreprise au Cameroun (SARL, SA, etc.).",
                Icon = "🏢",
                Duration = "3-4 semaines",
                Cost = "100000 FCFA",
                Difficulty = "MOYEN",
                StepCount = 7,
                CategoryId = "2"
            },
            // Fiscalité
            new Procedure
            {
                Id = "6",
                Title = "Déclaration d'Impôts",
                Description = "Déclarer vos revenus et payer vos impôts.",
                Icon = "🧮",
                Duration = "1 semaine",
                Cost = "Gratuit",
                Difficulty = "FACILE",
                StepCount = 3,
                CategoryId = "3"
            },
            // Éducation
            new Procedure
            {
                Id = "7",
                Title = "Inscription Universitaire",
                Description = "S'inscrire dans une université camerounaise.",
                Icon = "🎓",
                Duration = "2-3 semaines",
                Cost = "50000 FCFA",
                Difficulty = "MOYEN",
                StepCount = 6,
                CategoryId = "4"
            }
        };
    }
}

