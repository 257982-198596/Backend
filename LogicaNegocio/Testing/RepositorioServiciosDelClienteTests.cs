using Xunit;
using Moq;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Excepciones;

public class RepositorioServiciosDelClienteTests
{
    private DbContextOptions<CobrosContext> GetDbContextOptions()
    {
        return new DbContextOptionsBuilder<CobrosContext>()
            .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
            .Options;
    }

    [Fact]
    public void Add_ShouldAddServicioDelCliente_WhenValid()
    {
        // Arrange
        var options = GetDbContextOptions();
        using var context = new CobrosContext(options);
        var repo = new RepositorioServiciosDelCliente(context);

        var servicioDelCliente = new ServicioDelCliente
        {
            ServicioContratadoId = 1,
            ClienteId = 1,
            FrecuenciaDelServicioId = 1,
            MonedaDelServicioId = 1,
            Precio = 100,
            Descripcion = "Test Service"
        };

        context.Servicios.Add(new Servicio { Id = 1 });
        context.Clientes.Add(new Cliente { Id = 1, DocumentoId = 1, NumDocumento = "12345678" });
        context.Frecuencias.Add(new Frecuencia { Id = 1, Nombre = "Anual" });
        context.Monedas.Add(new Moneda { Id = 1 });
        context.EstadosServiciosDelClientes.Add(new EstadoServicioDelCliente { Nombre = "Activo" });
        context.SaveChanges();

        // Act
        repo.Add(servicioDelCliente);

        // Assert
        var addedService = context.ServiciosDelCliente.FirstOrDefault(s => s.Descripcion == "Test Service");
        Assert.NotNull(addedService);
        Assert.Equal(100, addedService.Precio);
    }

    [Fact]
    public void Add_ShouldNotAddServicioDelCliente_WhenInvalidFrecuencia()
    {
        // Arrange
        var options = GetDbContextOptions();
        using var context = new CobrosContext(options);
        var repo = new RepositorioServiciosDelCliente(context);

        var servicioDelCliente = new ServicioDelCliente
        {
            // Falta FrecuenciaDelServicioId 
            ServicioContratadoId = 1,
            Precio = 100,
            Descripcion = "Servicio Invalido",
            ClienteId = 1
        };

        // Act & Assert
        Assert.Throws<ServicioDelClienteException>(() => repo.Add(servicioDelCliente));
    }

    [Fact]
    public void Add_ShouldNotAddServicioDelCliente_WhenInvalidMoneda()
    {
        // Arrange
        var options = GetDbContextOptions();
        using var context = new CobrosContext(options);
        var repo = new RepositorioServiciosDelCliente(context);

        var servicioDelCliente = new ServicioDelCliente
        {
            // Falta Moneda
            ServicioContratadoId = 1,
            Precio = 100,
            Descripcion = "Servicio Invalido",
            ClienteId = 1,
            FrecuenciaDelServicioId = 1
        };

        // Act & Assert
        Assert.Throws<ServicioDelClienteException>(() => repo.Add(servicioDelCliente));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenRelatedEntitiesDoNotExist()
    {
        // Arrange
        var options = GetDbContextOptions();
        using var context = new CobrosContext(options);
        var repo = new RepositorioServiciosDelCliente(context);

        var servicioDelCliente = new ServicioDelCliente
        {
            ServicioContratadoId = 999, // Ids que no existen en la base de datos
            ClienteId = 999,
            FrecuenciaDelServicioId = 999,
            MonedaDelServicioId = 999,
            Precio = 100,
            Descripcion = "Non-existent Relations"
        };

        // Act & Assert
        Assert.Throws<ServicioDelClienteException>(() => repo.Add(servicioDelCliente));
    }


    [Fact]
    public void FindAll_ShouldReturnAllServiciosDelCliente()
    {
        // Arrange
        var options = GetDbContextOptions();
        using var context = new CobrosContext(options);

        // Clases Servicio
        var servicio = new Servicio { Id = 1, Nombre = "Servicio Base" };
        var cliente = new Cliente { Id = 1, DocumentoId = 1, NumDocumento = "12345678" };
        var frecuencia = new Frecuencia { Id = 1, Nombre = "Anual" };
        var moneda = new Moneda { Id = 1, Nombre = "USD" };
        var estado = new EstadoServicioDelCliente { Id = 1, Nombre = "Activo" };

        context.Servicios.Add(servicio);
        context.Clientes.Add(cliente);
        context.Frecuencias.Add(frecuencia);
        context.Monedas.Add(moneda);
        context.EstadosServiciosDelClientes.Add(estado);
        context.SaveChanges();

        // Servicio Del Cliente
        ServicioDelCliente serv1 = new ServicioDelCliente
        {
            Descripcion = "Servicio 1",
            ServicioContratadoId = 1,
            ClienteId = 1,
            FrecuenciaDelServicioId = 1,
            MonedaDelServicioId = 1,
            EstadoDelServicioDelCliente = estado
        };
        ServicioDelCliente serv2 = new ServicioDelCliente
        {
            Descripcion = "Servicio 2",
            ServicioContratadoId = 1,
            ClienteId = 1,
            FrecuenciaDelServicioId = 1,
            MonedaDelServicioId = 1,
            EstadoDelServicioDelCliente = estado
        };
        context.ServiciosDelCliente.AddRange(serv1, serv2);
        context.SaveChanges();

        var repository = new RepositorioServiciosDelCliente(context);

        // Act
        var result = repository.FindAll().OrderBy(s => s.Descripcion).ToList();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Servicio 1", result[0].Descripcion);
        Assert.Equal("Servicio 2", result[1].Descripcion);
    }
}