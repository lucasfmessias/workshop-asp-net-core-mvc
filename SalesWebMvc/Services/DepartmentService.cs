using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Usado para operações assíncronas
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context; // Dependência para SalesWebMvcContext (banco de dados)

        public DepartmentService(SalesWebMvcContext context) // Construtor para que a injeção de dependência possa ocorrer
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
