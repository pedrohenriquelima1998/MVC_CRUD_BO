using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL;
using System;

namespace Mvc_BO.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            // criei uma instância da classe AlunoBLL
            AlunoBLL _aluno = new AlunoBLL();
            // estou usando o método getAlunos e retornando
            // uma lista de alunos
            List<Aluno> alunos = _aluno.getAlunos().ToList();
            // passando para view
            return View(alunos);
        }

        //Get
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                AlunoBLL alunobll = new AlunoBLL();
                alunobll.IncluirAluno(aluno);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AlunoBLL alunobll = new AlunoBLL();
            Aluno aluno = alunobll.getAlunos().Single(x => x.Id == id);
            return View(aluno);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post([Bind(Exclude="Nome")]Aluno aluno)
        {
            AlunoBLL alunobll = new AlunoBLL();
            aluno.Nome = alunobll.getAlunos().Single(x => x.Id == aluno.Id).Nome;
            //Aluno aluno = alunobll.getAlunos().Single(x => x.Id == id);
            //UpdateModel(aluno,null,null, excludeProperties: new[] { "Nome" });

            if (ModelState.IsValid)
            {
                alunobll.AtualizarAluno(aluno);
                return RedirectToAction("Index");
            }
            return View(aluno);
        }
    
        [HttpPost]
        public ActionResult Delete(int id)
        {
            AlunoBLL alunobll = new AlunoBLL();
            alunobll.DeletarAluno(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                AlunoBLL alunobll = new AlunoBLL();
                alunobll.DeletarAluno(aluno.Id);
                return RedirectToAction("Index");
            }
            return View(aluno);
        }
    
        public ActionResult Details(int id)
        {
            AlunoBLL alunobll = new AlunoBLL();
            Aluno aluno = alunobll.getAlunos().Single(x => x.Id == id);
            return View(aluno);
        }

        public ActionResult Procurar(string procurarPor, string criterio)
        {
            AlunoBLL alunobll = new AlunoBLL();
            if(procurarPor == "Email")
            {
                Aluno aluno = alunobll.getAlunos().SingleOrDefault(x => x.Email == criterio || criterio == null);
                return View(aluno);
            }
            else
            {
                Aluno aluno = alunobll.getAlunos().SingleOrDefault(x => x.Nome == criterio || criterio == null);
                return View(aluno);
            }
        }

        public ActionResult Teste(int id=5)
        {
            ViewBag.Mensagem = "Id = " + id;
            return View();
        }


        public ActionResult Teste(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            return View();
        }

    
    }
}