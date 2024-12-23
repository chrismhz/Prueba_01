using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba1
{
    public class Class1
    {
        /*-- Tabla de Puestos
        CREATE TABLE Puesto(
            IdPuesto INT PRIMARY KEY,               -- IdPuesto es la clave primaria
            Nombre VARCHAR(50) NOT NULL             -- Nombre del puesto
        );

        -- Tabla de Operadores
        CREATE TABLE Operador(
            IdOperador INT PRIMARY KEY,             -- IdOperador es la clave primaria
            Nombre VARCHAR(50) NOT NULL,            -- Nombre del operador
            NoOperador INT UNIQUE,                  -- NoOperador con 8 dígitos y único
            Activo BIT NOT NULL,                    -- Activo o no(1 para activo, 0 para inactivo)
            FechaIngreso DATE NOT NULL,             -- Fecha de ingreso
            IdPuesto INT,                           -- Relación con la tabla Puesto
            RowCreatedAt DATETIME DEFAULT GETDATE(), -- Fecha de creación automática
            FOREIGN KEY(IdPuesto) REFERENCES Puesto(IdPuesto) -- Relación con la tabla Puesto
        );

        -- Agregar registros de ejemplo

        -- Registros de puestos
        INSERT INTO Puesto(IdPuesto, Nombre) VALUES(1, 'Gerente');
                INSERT INTO Puesto(IdPuesto, Nombre) VALUES(2, 'Operador');
                INSERT INTO Puesto(IdPuesto, Nombre) VALUES(3, 'Supervisor');

        -- Registros de operadores
        INSERT INTO Operador(IdOperador, Nombre, NoOperador, Activo, FechaIngreso, IdPuesto)
        VALUES(1, 'Juan Pérez', 12345678, 1, '2024-01-01', 1);

                INSERT INTO Operador(IdOperador, Nombre, NoOperador, Activo, FechaIngreso, IdPuesto)
        VALUES(2, 'Ana López', 23456789, 0, '2023-06-15', 2);

                INSERT INTO Operador(IdOperador, Nombre, NoOperador, Activo, FechaIngreso, IdPuesto)
        VALUES(3, 'Carlos Ruiz', 34567890, 1, '2024-03-10', 3);*/


        /*
         
        namespace ML
        {
            public class Puesto
            {
                public int IdPuesto { get; set; }
                public string Nombre { get; set; }
            }

            public class Operador
            {
                public int IdOperador { get; set; }
                public string Nombre { get; set; }
                public int NoOperador { get; set; }
                public bool Activo { get; set; }
                public DateTime FechaIngreso { get; set; }
                public int IdPuesto { get; set; }
                public Puesto Puesto { get; set; }
            }
        } 

         */


        /*
         
        using System;
        using System.Collections.Generic;
        using System.Data.SqlClient;
        using System.Linq;
        using System.Web.Mvc;
        using ML;

        namespace YourApp.Controllers
        {
            public class OperadorController : Controller
            {
                // GET: Operador
                public ActionResult GetAll()
                {
                    ML.Result result = BL.Operador.GetAll();
                    if (result.Correct)
                    {
                        return View(result.Objects);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = result.ErrorMessage;
                        return View();
                    }
                }

                // GET: Operador/Details/5
                public ActionResult GetById(int idOperador)
                {
                    ML.Result result = BL.Operador.GetById(idOperador);
                    if (result.Correct)
                    {
                        return View(result.Object);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = result.ErrorMessage;
                        return View();
                    }
                }

                // POST: Operador/Add
                [HttpPost]
                public ActionResult Add(Operador operador)
                {
                    ML.Result result = BL.Operador.Add(operador);
                    if (result.Correct)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = result.ErrorMessage;
                        return View(operador);
                    }
                }

                // POST: Operador/Update
                [HttpPost]
                public ActionResult Update(Operador operador)
                {
                    ML.Result result = BL.Operador.Update(operador);
                    if (result.Correct)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = result.ErrorMessage;
                        return View(operador);
                    }
                }

                // POST: Operador/Delete/5
                [HttpPost]
                public ActionResult Delete(int idOperador)
                {
                    ML.Result result = BL.Operador.Delete(idOperador);
                    if (result.Correct)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = result.ErrorMessage;
                        return RedirectToAction("GetAll");
                    }
                }
            }
        }
 

         */


        /*
         
        namespace BL
{
    public class Operador
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Operador", context))
                    {
                        context.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<ML.Operador> operadores = new List<ML.Operador>();

                        while (reader.Read())
                        {
                            ML.Operador operador = new ML.Operador
                            {
                                IdOperador = (int)reader["IdOperador"],
                                Nombre = reader["Nombre"].ToString(),
                                NoOperador = (int)reader["NoOperador"],
                                Activo = (bool)reader["Activo"],
                                FechaIngreso = (DateTime)reader["FechaIngreso"],
                                IdPuesto = (int)reader["IdPuesto"],
                                Puesto = new ML.Puesto { IdPuesto = (int)reader["IdPuesto"] }
                            };
                            operadores.Add(operador);
                        }
                        result.Objects = operadores;
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int idOperador)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Operador WHERE IdOperador = @IdOperador", context))
                    {
                        cmd.Parameters.AddWithValue("@IdOperador", idOperador);
                        context.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            ML.Operador operador = new ML.Operador
                            {
                                IdOperador = (int)reader["IdOperador"],
                                Nombre = reader["Nombre"].ToString(),
                                NoOperador = (int)reader["NoOperador"],
                                Activo = (bool)reader["Activo"],
                                FechaIngreso = (DateTime)reader["FechaIngreso"],
                                IdPuesto = (int)reader["IdPuesto"],
                                Puesto = new ML.Puesto { IdPuesto = (int)reader["IdPuesto"] }
                            };
                            result.Object = operador;
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Operador operador)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Operador (Nombre, NoOperador, Activo, FechaIngreso, IdPuesto) VALUES (@Nombre, @NoOperador, @Activo, @FechaIngreso, @IdPuesto)", context))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", operador.Nombre);
                        cmd.Parameters.AddWithValue("@NoOperador", operador.NoOperador);
                        cmd.Parameters.AddWithValue("@Activo", operador.Activo);
                        cmd.Parameters.AddWithValue("@FechaIngreso", operador.FechaIngreso);
                        cmd.Parameters.AddWithValue("@IdPuesto", operador.IdPuesto);

                        context.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int idOperador)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Operador WHERE IdOperador = @IdOperador", context))
                    {
                        cmd.Parameters.AddWithValue("@IdOperador", idOperador);

                        context.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontró el operador para eliminar.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Operador operador)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Operador SET Nombre = @Nombre, NoOperador = @NoOperador, Activo = @Activo, FechaIngreso = @FechaIngreso, IdPuesto = @IdPuesto WHERE IdOperador = @IdOperador", context))
                    {
                        cmd.Parameters.AddWithValue("@IdOperador", operador.IdOperador);
                        cmd.Parameters.AddWithValue("@Nombre", operador.Nombre);
                        cmd.Parameters.AddWithValue("@NoOperador", operador.NoOperador);
                        cmd.Parameters.AddWithValue("@Activo", operador.Activo);
                        cmd.Parameters.AddWithValue("@FechaIngreso", operador.FechaIngreso);
                        cmd.Parameters.AddWithValue("@IdPuesto", operador.IdPuesto);

                        context.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontró el operador para actualizar.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}

         */


        /*
         
        @model ML.Operador
@{
    ViewBag.Title = "Registro de Operador";
    Layout = "~/Views/Shared/_Layout.cshtml";
}
@Html.ValidationSummary(true, "", new { @class = "text-danger" })

<div class="container">
    <div class="row">
        <div class="col-12">
            <h2 class="text-center">Registro de Operador</h2>
            <center><h5>Formulario para registrar un nuevo operador.</h5></center>
            <hr />
        </div>
    </div>

    <div class="row">
        <div class="col-12">
            @using (Html.BeginForm("RegistrarOperador", "Operador", FormMethod.Post, new { enctype = "multipart/form-data" }))
            {
                @Html.AntiForgeryToken()

                <div class="row mb-6">
                    <div class="col-md-6">
                        @Html.LabelFor(model => model.Nombre, new { @class = "form-label" })
                        @Html.TextBoxFor(model => model.Nombre, new { @class = "form-control" })
                        @Html.ValidationMessageFor(model => model.Nombre, "", new { @class = "text-danger" })
                    </div>

                    <div class="col-md-6">
                        @Html.LabelFor(model => model.NoOperador, new { @class = "form-label" })
                        @Html.TextBoxFor(model => model.NoOperador, new { @class = "form-control", maxlength = "8" })
                        @Html.ValidationMessageFor(model => model.NoOperador, "", new { @class = "text-danger" })
                    </div>
                </div>

                <div class="row mb-6">
                    <div class="col-md-6">
                        @Html.LabelFor(model => model.Activo, new { @class = "form-label" })
                        <div>
                            @Html.RadioButtonFor(model => model.Activo, true) @Html.Label("Activo")
                            @Html.RadioButtonFor(model => model.Activo, false) @Html.Label("Inactivo")
                        </div>
                        @Html.ValidationMessageFor(model => model.Activo, "", new { @class = "text-danger" })
                    </div>

                    <div class="col-md-6">
                        @Html.LabelFor(model => model.FechaIngreso, new { @class = "form-label" })
                        @Html.TextBoxFor(model => model.FechaIngreso, new { @class = "form-control", type = "date" })
                        @Html.ValidationMessageFor(model => model.FechaIngreso, "", new { @class = "text-danger" })
                    </div>
                </div>

                <div class="row mb-6">
                    <div class="col-md-6">
                        @Html.LabelFor(model => model.IdPuesto, new { @class = "form-label" })
                        @Html.DropDownListFor(model => model.IdPuesto, new SelectList(Model.Puestos, "IdPuesto", "Nombre"), "Seleccione un Puesto", new { @class = "form-control" })
                        @Html.ValidationMessageFor(model => model.IdPuesto, "", new { @class = "text-danger" })
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <button type="submit" class="btn btn-success">Guardar</button>
                        @Html.ActionLink("Cancelar", "GetAll", "Operador", new { @class = "btn btn-danger" })
                    </div>
                </div>
            }
        </div>
    </div>
</div>

<style>
    input.valid {
        border-color: green;
    }

    input.invalid {
        border-color: red;
    }
</style>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}

         
         */


        /*
         
        <!-- DevExpress CDN -->
        <link href="https://cdn3.devexpress.com/jslib/22.1.5/css/dx.light.css" rel="stylesheet" />
        <script src="https://cdn3.devexpress.com/jslib/22.1.5/js/dx.all.js"></script>

        <!-- SweetAlert2 CDN -->
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


        @model IEnumerable<ML.Operador>
        @{
            ViewBag.Title = "Operadores";
        }

        <h2>Operadores</h2>

        <!-- dxDataGrid -->
        <div id="gridContainer"></div>

        <script>
            $(function () {
                // dxDataGrid Configuration
                $("#gridContainer").dxDataGrid({
                    dataSource: @Html.Raw(Json.Serialize(Model)),
                    columns: [
                        { dataField: "IdOperador", caption: "ID Operador", width: 100 },
                        { dataField: "Nombre", caption: "Nombre", width: 200 },
                        { dataField: "NoOperador", caption: "No. Operador", width: 150 },
                        { dataField: "Activo", caption: "Activo", width: 100 },
                        { dataField: "FechaIngreso", caption: "Fecha de Ingreso", width: 150 },
                        {
                            type: "buttons", buttons: [
                                {
                                    hint: "Editar",
                                    icon: "edit",
                                    onClick: function (e) {
                                        var data = e.row.data;
                                        editarOperador(data);
                                    }
                                },
                                {
                                    hint: "Eliminar",
                                    icon: "delete",
                                    onClick: function (e) {
                                        var data = e.row.data;
                                        eliminarOperador(data);
                                    }
                                }
                            ]
                        }
                    ],
                    paging: { pageSize: 10 },
                    pager: { showPageSizeSelector: true, allowedPageSizes: [10, 20, 50] },
                    filterRow: { visible: true, applyFilter: "auto" }
                });
            });

            // Función para editar un operador
            function editarOperador(operador) {
                Swal.fire({
                    title: 'Editar Operador',
                    html:
                        '<input id="swal-input1" class="swal2-input" value="' + operador.Nombre + '" placeholder="Nombre">' +
                        '<input id="swal-input2" class="swal2-input" value="' + operador.NoOperador + '" placeholder="No. Operador">' +
                        '<input id="swal-input3" class="swal2-input" value="' + operador.FechaIngreso + '" placeholder="Fecha Ingreso">',
                    showCancelButton: true,
                    confirmButtonText: 'Guardar',
                    cancelButtonText: 'Cancelar',
                    preConfirm: () => {
                        return {
                            Nombre: $('#swal-input1').val(),
                            NoOperador: $('#swal-input2').val(),
                            FechaIngreso: $('#swal-input3').val(),
                            IdOperador: operador.IdOperador
                        };
                    }
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Llamada AJAX para guardar el operador editado
                        $.ajax({
                            url: '@Url.Action("EditarOperador", "Operador")',
                            type: 'POST',
                            data: result.value,
                            success: function (response) {
                                Swal.fire('Guardado!', '', 'success');
                                location.reload(); // Recargar la grid
                            },
                            error: function () {
                                Swal.fire('Error', 'No se pudo guardar el operador', 'error');
                            }
                        });
                    }
                });
            }

            // Función para eliminar un operador
            function eliminarOperador(operador) {
                Swal.fire({
                    title: '¿Estás seguro?',
                    text: "No podrás revertir esto!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: '@Url.Action("EliminarOperador", "Operador")',
                            type: 'POST',
                            data: { idOperador: operador.IdOperador },
                            success: function (response) {
                                Swal.fire('Eliminado!', '', 'success');
                                location.reload(); // Recargar la grid
                            },
                            error: function () {
                                Swal.fire('Error', 'No se pudo eliminar el operador', 'error');
                            }
                        });
                    }
                });
            }

            // Función para agregar un operador
            function agregarOperador() {
                Swal.fire({
                    title: 'Agregar Operador',
                    html:
                        '<input id="swal-input1" class="swal2-input" placeholder="Nombre">' +
                        '<input id="swal-input2" class="swal2-input" placeholder="No. Operador">' +
                        '<input id="swal-input3" class="swal2-input" placeholder="Fecha Ingreso">',
                    showCancelButton: true,
                    confirmButtonText: 'Guardar',
                    cancelButtonText: 'Cancelar',
                    preConfirm: () => {
                        return {
                            Nombre: $('#swal-input1').val(),
                            NoOperador: $('#swal-input2').val(),
                            FechaIngreso: $('#swal-input3').val()
                        };
                    }
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Llamada AJAX para agregar un nuevo operador
                        $.ajax({
                            url: '@Url.Action("AgregarOperador", "Operador")',
                            type: 'POST',
                            data: result.value,
                            success: function (response) {
                                Swal.fire('Guardado!', '', 'success');
                                location.reload(); // Recargar la grid
                            },
                            error: function () {
                                Swal.fire('Error', 'No se pudo agregar el operador', 'error');
                            }
                        });
                    }
                });
            }

            // Botón para agregar operador
            $(".add-operator-btn").on("click", function () {
                agregarOperador();
            });
        </script>

        <button class="add-operator-btn btn btn-primary">Agregar Operador</button>

         */

        /*
         
        public class OperadorController : Controller
        {
            private readonly IOperadorService _operadorService;

            public OperadorController(IOperadorService operadorService)
            {
                _operadorService = operadorService;
            }

            // Acción para obtener los operadores
            public IActionResult Index()
            {
                var operadores = _operadorService.ObtenerOperadores();
                return View(operadores);
            }

            // Acción para agregar un operador
            [HttpPost]
            public IActionResult AgregarOperador(Operador operador)
            {
                if (ModelState.IsValid)
                {
                    _operadorService.AgregarOperador(operador);
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }

            // Acción para editar un operador
            [HttpPost]
            public IActionResult EditarOperador(Operador operador)
            {
                if (ModelState.IsValid)
                {
                    _operadorService.EditarOperador(operador);
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }

            // Acción para eliminar un operador
            [HttpPost]
            public IActionResult EliminarOperador(int idOperador)
            {
                _operadorService.EliminarOperador(idOperador);
                return Json(new { success = true });
            }
        }

         
         */


        /*
         
         public class OperadorService : IOperadorService
        {
            private readonly ApplicationDbContext _context;

            public OperadorService(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<Operador> ObtenerOperadores()
            {
                return _context.Operadores.ToList();
            }

            public void AgregarOperador(Operador operador)
            {
                _context.Operadores.Add(operador);
                _context.SaveChanges();
            }

            public void EditarOperador(Operador operador)
            {
                _context.Operadores.Update(operador);
                _context.SaveChanges();
            }

            public void EliminarOperador(int idOperador)
            {
                var operador = _context.Operadores.Find(idOperador);
                if (operador != null)
                {
                    _context.Operadores.Remove(operador);
                    _context.SaveChanges();
                }
            }
        }

         
         */
    }
}
