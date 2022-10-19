﻿using LanchesWill.Models;
using LanchesWill.Repositories.Interfaces;
using LanchesWill.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesWill.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRespository _lancheRepository;

        public LancheController(ILancheRespository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string cateogiraAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                cateogiraAtual = "TODOS OS LANCHES";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(l => l.Nome);
                cateogiraAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = cateogiraAtual
            };

            return View(lanchesListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lancheDetails = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

            return View(lancheDetails);
        }
    }
}
