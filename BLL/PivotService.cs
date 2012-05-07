using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using DAL;

namespace BLL
{
    public class PivotService
    {
        private PivotEntitiesContainer Containner { get; set; }

        public PivotService()
        {
            this.Containner = new PivotEntitiesContainer();
        }

        #region Usuarios

        public bool ValidateUser(string userName, string password)
        {
            var hashPass = GetSHA1(password);
            return
                Containner.Personas.Where(
                    u =>
                    u.CorreoElectronico.Equals(userName, StringComparison.InvariantCultureIgnoreCase) &&
                    u.Clave.Equals(password)).Count() == 1;
        }

        public IEnumerable<Personas> GetPersonaByEmail(string mail)
        {
            var query = from pe in Containner.Personas
                        join usu in Containner.Usuarios on pe.PersonasID equals usu.PersonasID
                        where pe.CorreoElectronico == mail
                        select pe;

            return (query).ToList();
        }

        public IEnumerable<Personas> GetPersonByUser()
        {
            var query = from PE in Containner.Personas
                        join US in Containner.Usuarios
                            on PE.PersonasID equals US.PersonasID
                        select PE;

            return query.ToList();
        }

        public static string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            var encoding = new ASCIIEncoding();
            byte[] stream = null;
            var sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            foreach (byte t in stream)
                sb.AppendFormat("{0:x2}", t);
            return sb.ToString();
        }

        #endregion

        #region Personas

        public IEnumerable<Personas> GetAllPersonas()
        {
            return this.Containner.Personas.ToList();
        }

        public bool AddPersona(Personas persona)
        {
            //  Por ser la primera vez de ingreso la contraseña NO se encripta
            //persona.Clave = GetSHA1(persona.Clave);
            Containner.AddToPersonas(persona);
            return Containner.SaveChanges() > 0;
        }

        public Personas GetPersonaById(int id)
        {
            return Containner.Personas.OfType<Personas>().Where(a => a.PersonasID.Equals(id)).FirstOrDefault();
        }

        public bool EditPersona(Personas page)
        {
            //  Por ser la primera vez de ingreso la contraseña NO se encripta
            //page.Clave = GetSHA1(page.Clave);
            return Containner.SaveChanges() > 0;
        }

        public void DeletePersona(int id)
        {
            var persona = GetPersonaById(id);
            Containner.DeleteObject(persona);
            Containner.SaveChanges();
        }

        #endregion

        #region Areas

        public IEnumerable<Areas> GetAllAreas()
        {
            return this.Containner.Areas.ToList();
        }

        public bool AddAreas(Areas area)
        {
            Containner.AddToAreas(area);
            return Containner.SaveChanges() > 0;
        }

        public Areas GetAreaById(int id)
        {
            return Containner.Areas.OfType<Areas>().Where(a => a.AreasID.Equals(id)).FirstOrDefault();
        }

        public bool EditArea(Areas page)
        {
            return Containner.SaveChanges() > 0;
        }

        public void DeleteArea(int id)
        {
            var area = GetAreaById(id);
            Containner.DeleteObject(area);
            Containner.SaveChanges();
        }

        #endregion

        #region Formas

        public IEnumerable<Formas> GetAllFormas()
        {
            return this.Containner.Formas.ToList();
        }

        public bool AddFormas(Formas forma)
        {
            Containner.AddToFormas(forma);
            return Containner.SaveChanges() > 0;
        }

        public Formas GetFormaById(int id)
        {
            return Containner.Formas.OfType<Formas>().Where(a => a.FormaID.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Formas> GetFormaListById(int id)
        {
            var query = from formas in Containner.Formas
                        where formas.ProgramaID == id
                        select formas;

            return (query).ToList();
        }

        public bool EditForma(Formas page)
        {
            return Containner.SaveChanges() > 0;
        }

        public void DeleteForma(int id)
        {
            var forma = GetFormaById(id);
            Containner.DeleteObject(forma);
            Containner.SaveChanges();
        }

        #endregion

        #region Programas

        public IEnumerable<Programas> GetAllProgramas()
        {
            return this.Containner.Programas.ToList();
        }

        public bool AddProgramas(Programas programa)
        {
            Containner.AddToProgramas(programa);
            return Containner.SaveChanges() > 0;
        }

        public Programas GetProgramaById(int id)
        {
            return Containner.Programas.OfType<Programas>().Where(a => a.ProgramaID.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Programas> GetProgramaListById(int id)
        {
            var query = from programas in Containner.Programas
                        where programas.CategoriaID == id
                        select programas;

            return (query).ToList();
        }

        public bool EditPrograma(Programas page)
        {
            return Containner.SaveChanges() > 0;
        }

        public void DeletePrograma(int id)
        {
            var programa = GetProgramaById(id);
            Containner.DeleteObject(programa);
            Containner.SaveChanges();
        }

        #endregion

        #region Categorias

        public IEnumerable<Categorias> GetAllCategorias()
        {
            return this.Containner.Categorias.ToList();
        }
        public bool AddCategorias(Categorias categoria)
        {
            Containner.AddToCategorias(categoria);
            return Containner.SaveChanges() > 0;
        }
        public Categorias GetCategoriaById(int id)
        {
            return Containner.Categorias.OfType<Categorias>().Where(a => a.CategoriaID.Equals(id)).FirstOrDefault();
        }

        public bool EditCategorias(Categorias page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteCategoria(int id)
        {
            var categoria = GetCategoriaById(id);
            Containner.DeleteObject(categoria);
            Containner.SaveChanges();
        }

        #endregion

        #region Dimensiones

        public IEnumerable<Dimensiones> GetAllDimensiones()
        {
            return this.Containner.Dimensiones.ToList();
        }
        public bool AddDimensiones(Dimensiones dimension)
        {
            Containner.AddToDimensiones(dimension);
            return Containner.SaveChanges() > 0;
        }
        public Dimensiones GetDimensionById(int id)
        {
            return Containner.Dimensiones.OfType<Dimensiones>().Where(a => a.DimensionID.Equals(id)).FirstOrDefault();
        }

        public bool EditDimensiones(Dimensiones page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteDimension(int id)
        {
            var dimension = GetDimensionById(id);
            Containner.DeleteObject(dimension);
            Containner.SaveChanges();
        }

        #endregion

        #region Tipo Empresas

        public IEnumerable<TipoEmpresas> GetAllTipoEmpresas()
        {
            return this.Containner.TipoEmpresas.ToList();
        }
        public bool AddTipoEmpresa(TipoEmpresas tipoempresa)
        {
            Containner.AddToTipoEmpresas(tipoempresa);
            return Containner.SaveChanges() > 0;
        }
        public TipoEmpresas GetTipoEmpresaById(int id)
        {
            return Containner.TipoEmpresas.OfType<TipoEmpresas>().Where(a => a.TipoEmpresaID.Equals(id)).FirstOrDefault();
        }

        public bool EditTipoEmpresa(TipoEmpresas page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteTipoEmpresa(int id)
        {
            var tipoEmpresa = GetTipoEmpresaById(id);
            Containner.DeleteObject(tipoEmpresa);
            Containner.SaveChanges();
        }

        #endregion

        #region Empresas

        public IEnumerable<Empresas> GetAllEmpresas()
        {
            return this.Containner.Empresas.ToList();
        }
        public bool AddEmpresa(Empresas empresa)
        {
            Containner.AddToEmpresas(empresa);
            return Containner.SaveChanges() > 0;
        }
        public Empresas GetEmpresaById(int id)
        {
            return Containner.Empresas.OfType<Empresas>().Where(a => a.EmpresaID.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Empresas> GetEmpresaListById(int id)
        {
            var query = from empresas in Containner.Empresas
                        where empresas.TipoEmpresaID == id
                        select empresas;

            return (query).ToList();
        }

        public bool EditEmpresa(Empresas page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteEmpresa(int id)
        {
            var empresa = GetEmpresaById(id);
            Containner.DeleteObject(empresa);
            Containner.SaveChanges();
        }

        #endregion

        #region Proyectos

        public IEnumerable<Proyectos> GetAllProyectos()
        {
            return this.Containner.Proyectos.ToList();
        }
        public bool AddProyecto(Proyectos proyecto)
        {
            Containner.AddToProyectos(proyecto);
            return Containner.SaveChanges() > 0;
        }
        public Proyectos GetProyectoById(int id)
        {
            return Containner.Proyectos.OfType<Proyectos>().Where(a => a.ProyectoID.Equals(id)).FirstOrDefault();
        }

        public bool EditProyecto(Proyectos page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteProyecto(int id)
        {
            var proyecto = GetProyectoById(id);
            Containner.DeleteObject(proyecto);
            Containner.SaveChanges();
        }

        public bool AddResponsablesEmp(ResponsablesEmp responsableEmp)
        {
            Containner.AddToResponsablesEmp(responsableEmp);
            return Containner.SaveChanges() > 0;
        }

        #endregion

        #region Macro Procesos

        public IEnumerable<MacroProcesos> GetAllMacroProcesos()
        {
            return this.Containner.MacroProcesos.ToList();
        }
        public bool AddMacroProceso(MacroProcesos macroproceso)
        {
            Containner.AddToMacroProcesos(macroproceso);
            return Containner.SaveChanges() > 0;
        }
        public MacroProcesos GetMacroProcesoById(int id)
        {
            return Containner.MacroProcesos.OfType<MacroProcesos>().Where(a => a.MacroProcesosID.Equals(id)).FirstOrDefault();
        }

        public bool EditMacroProceso(MacroProcesos page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteMacroProceso(int id)
        {
            var macroproceso = GetMacroProcesoById(id);
            Containner.DeleteObject(macroproceso);
            Containner.SaveChanges();
        }

        #endregion

        #region Objetos

        public IEnumerable<Objetos> GetAllObjetos()
        {
            return this.Containner.Objetos.ToList();
        }

        public bool AddObjetos(Objetos objeto)
        {
            Containner.AddToObjetos(objeto);
            return Containner.SaveChanges() > 0;
        }

        public Objetos GetObjetoById(int id)
        {
            return Containner.Objetos.OfType<Objetos>().Where(a => a.ObjetoID.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Objetos> GetObjetoListById(int id)
        {
            var query = from objeto in Containner.Objetos
                        where objeto.FormaID == id
                        select objeto;

            return (query).ToList();
        }

        public bool EditObjeto(Objetos page)
        {
            return Containner.SaveChanges() > 0;
        }

        public void DeleteObjetos(int id)
        {
            var objeto = GetObjetoById(id);
            Containner.DeleteObject(objeto);
            Containner.SaveChanges();
        }

        #endregion

        #region Paises

        public IEnumerable<Paises> GetAllPaises()
        {
            return this.Containner.Paises.ToList();
        }

        public bool AddPaises(Paises pais)
        {
            Containner.AddToPaises(pais);
            return Containner.SaveChanges() > 0;
        }

        public Paises GetPaisId(int id)
        {
            return Containner.Paises.OfType<Paises>().Where(a => a.PaisesID.Equals(id)).FirstOrDefault();
        }

        public bool EditPaise(Paises page)
        {
            return Containner.SaveChanges() > 0;
        }

        public void DeletePaises(int id)
        {
            var paises = GetPaisId(id);
            Containner.DeleteObject(paises);
            Containner.SaveChanges();
        }

        #endregion

        #region Procesos

        public IEnumerable<Procesos> GetAllProcesos()
        {
            return this.Containner.Procesos.ToList();
        }

        public bool AddProcesos(Procesos proceso)
        {
            Containner.AddToProcesos(proceso);
            return Containner.SaveChanges() > 0;
        }

        public Procesos GetProcesoById(int id)
        {
            return Containner.Procesos.OfType<Procesos>().Where(a => a.ProcesosID.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Procesos> GetProcesosListById(int id)
        {
            var query = from procesos in Containner.Procesos
                        where procesos.MacroProcesosID == id
                        select procesos;

            return (query).ToList();
        }

        public bool EditProceso(Procesos page)
        {
            return Containner.SaveChanges() > 0;
        }

        public void DeleteProceso(int id)
        {
            var proceso = GetProcesoById(id);
            Containner.DeleteObject(proceso);
            Containner.SaveChanges();
        }

        #endregion

        #region Cargos

        public IEnumerable<Cargos> GetAllCargos()
        {
            return this.Containner.Cargos.ToList();
        }

        public bool AddCargos(Cargos cargo)
        {
            Containner.AddToCargos(cargo);
            return Containner.SaveChanges() > 0;
        }

        public Cargos GetCargoById(int id)
        {
            return Containner.Cargos.OfType<Cargos>().Where(a => a.CargosID.Equals(id)).FirstOrDefault();
        }

        public bool EditCargos(Cargos page)
        {
            return Containner.SaveChanges() > 0;
        }

        public void DeleteCargo(int id)
        {
            var cargo = GetProcesoById(id);
            Containner.DeleteObject(cargo);
            Containner.SaveChanges();
        }

        #endregion

        #region Usuarios

        public IEnumerable<Usuarios> GetAllUsuarios()
        {
            return this.Containner.Usuarios.ToList();
        }
        public bool AddUsuarios(Usuarios usuario)
        {
            Containner.AddToUsuarios(usuario);
            return Containner.SaveChanges() > 0;
        }
        public Usuarios GetUsuarioById(int id)
        {
            return Containner.Usuarios.OfType<Usuarios>().Where(a => a.UsuariosID.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Usuarios> GetUsuarioByPerson(int idPerson)
        {
            var query = from US in Containner.Usuarios
                        where US.PersonasID == idPerson
                        select US;

            return query.ToList();
        }

        public bool EditUsuarios(Usuarios page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteUsuario(int id)
        {
            var usuario = GetDimensionById(id);
            Containner.DeleteObject(usuario);
            Containner.SaveChanges();
        }

        #endregion

        #region Perfiles Cargo

        public IEnumerable<PerfilCargos> GetAllPerfilCargos()
        {
            return this.Containner.PerfilCargos.ToList();
        }
        public bool AddUPerfilCargo(PerfilCargos perfilCargo)
        {
            Containner.AddToPerfilCargos(perfilCargo);
            return Containner.SaveChanges() > 0;
        }
        public PerfilCargos GetPerfilCargoById(int id)
        {
            return Containner.PerfilCargos.OfType<PerfilCargos>().Where(a => a.PerfilCargosID.Equals(id)).FirstOrDefault();
        }

        public bool EditPerfilCargo(PerfilCargos page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeletePerfilCargo(int id)
        {
            var perfilcargo = GetDimensionById(id);
            Containner.DeleteObject(perfilcargo);
            Containner.SaveChanges();
        }

        #endregion

        #region Grupos

        public IEnumerable<Grupos> GetAllGrupos()
        {
            return this.Containner.Grupos.ToList();
        }
        public bool AddGrupo(Grupos grupo)
        {
            Containner.AddToGrupos(grupo);
            return Containner.SaveChanges() > 0;
        }
        public Grupos GetGrupoById(int id)
        {
            return Containner.Grupos.OfType<Grupos>().Where(a => a.GruposId.Equals(id)).FirstOrDefault();
        }

        public bool EditGrupo(Grupos page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteGrupo(int id)
        {
            var grupo = GetGrupoById(id);
            Containner.DeleteObject(grupo);
            Containner.SaveChanges();
        }

        #endregion

        #region Grupos Pertenece

        public IEnumerable<GruposPertenece> GetAllGruposPertenece()
        {
            return this.Containner.GruposPertenece.ToList();
        }
        public bool AddGruposPertenece(GruposPertenece grupo)
        {
            Containner.AddToGruposPertenece(grupo);
            return Containner.SaveChanges() > 0;
        }
        public GruposPertenece GetGruposPerteneceById(int id)
        {
            return Containner.GruposPertenece.OfType<GruposPertenece>().Where(a => a.GruposPerteneceID.Equals(id)).FirstOrDefault();
        }

        public bool EditGruposPertenece(GruposPertenece page)
        {
            return Containner.SaveChanges() > 0;
        }
        public void DeleteGruposPertenece(int id)
        {
            var grupo = GetGruposPerteneceById(id);
            Containner.DeleteObject(grupo);
            Containner.SaveChanges();
        }

        public IEnumerable<GruposPertenece> GetGruposPerteneceListById(int id)
        {
            var query = from grupoP in Containner.GruposPertenece
                        where grupoP.PersonasID == id
                        select grupoP;

            return (query).ToList();
        }

        #endregion

        #region Comprobantes

        public bool addTipoDeComprobantes(TipoDeComprobantes tipoComp)
        {
            Containner.AddToTipoDeComprobantes(tipoComp);
            return Containner.SaveChanges() > 0;
        }
        #endregion

        #region Centros de Operacion

        public bool AddCentroOP(CentroOperacion centOP)
        {
            Containner.AddToCentroOperacion(centOP);
            return Containner.SaveChanges() > 0;
        }

        public IEnumerable<CentroOperacion> GetAllCentrosOP()
        {
            return this.Containner.CentroOperacion.ToList();
        }
        #endregion

        #region CuentasPuc

        public IEnumerable<CuentasPuc> GetAllCuentasPuc()
        {
            return Containner.CuentasPuc.ToList();
        }

        public bool AddCuentaPUC(CuentasPuc cuentaPuc)
        {
            Containner.AddToCuentasPuc(cuentaPuc);
            return Containner.SaveChanges() > 0;
        }

        #endregion

        #region Tipos De Pago

        public IEnumerable<TiposDePago> GetAllTiposDePago()
        {
            return Containner.TiposDePago.ToList();
        }

        #endregion

        #region Auxiliares

        public IEnumerable<Auxiliares> GetAllAuxiliares()
        {
            return Containner.Auxiliares.ToList();
        }

        #endregion

        #region TipoAuxiliares

        public IEnumerable<TipoAuxiliares> GetAllTipoAuxiliar()
        {
            return Containner.TipoAuxiliares.ToList();
        }
        #endregion

        #region Cuenta

        public bool addCuenta(Cuentas cuenta)
        {
            Containner.AddToCuentas(cuenta);
            return Containner.SaveChanges() > 0;
        }

        public bool EditCuenta(Cuentas cuenta)
        {
            return Containner.SaveChanges() > 0;
        }

        #endregion

        #region Auxiliares

        public Auxiliares GetAxiliaresById(int id)
        {
            return Containner.Auxiliares.OfType<Auxiliares>().Where(a => a.AuxilaresID.Equals(id)).FirstOrDefault();
        }

        public bool EditAuxiliares(Auxiliares aux)
        {
            return Containner.SaveChanges() > 0;
        }

        #endregion
    }
}
