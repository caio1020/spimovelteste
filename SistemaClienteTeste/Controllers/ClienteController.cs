using Microsoft.AspNetCore.Mvc;
using SistemaClienteTeste.Models;
using SistemaClienteTeste.Repositorio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio; 
        }

        public async Task<IActionResult> Index()
        {
            List<ClienteModel> clientes = await _clienteRepositorio.BuscarTodos();

            return View(clientes);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            ClienteModel cliente = await _clienteRepositorio.BuscarPorID(id);
            return View(cliente);
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            ClienteModel cliente = await _clienteRepositorio.BuscarPorID(id);
            return View(cliente);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _clienteRepositorio.Apagar(id);

                if(apagado) TempData["MensagemSucesso"] = "Cliente apagado com sucesso!"; else TempData["MensagemErro"] = "Ops, não conseguimos cadastrar seu cliente, tente novamante!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu cliente, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ClienteModel clienteModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clienteModel = await _clienteRepositorio.Adicionar(clienteModel);

                    TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(clienteModel);
            }
            catch (Exception erro)
            { 
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu cliente, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ClienteModel clienteModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clienteModel = await _clienteRepositorio.Atualizar(clienteModel);
                    TempData["MensagemSucesso"] = "Cliente alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(clienteModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu cliente, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
