using System;
using System.Collections.Generic;

public class Pedido
{
    private List<Producto> _productos = new List<Producto>();
    public string Estado { get; private set; }
    public decimal Descuento { get; private set; } = 0m;

    public Pedido()
    {
        Estado = "Pendiente";
    }

    public bool AgregarProducto(Producto p, int cantidad)
    {
        if (p.DescontarStock(cantidad))
        {
            for (int i = 0; i < cantidad; i++)
            {
                _productos.Add(p);
            }
            return true;
        }
        return false;
    }

    
    public bool AplicarCupon(string codigo)
    {
        if (codigo != null && codigo.Trim().ToUpper() == "DESCUENTO10")
        {
            Descuento = 0.10m;
            return true;
        }
        return false;
    }

    
    public void VaciarCarrito()
    {
        foreach (Producto p in _productos)
        {
            p.DevolverStock(1);
        }
        _productos.Clear();
        Descuento = 0m;
    }

    public decimal CalcularTotal(decimal impuesto)
    {
        decimal subtotal = 0;
        foreach (Producto p in _productos)
        {
            subtotal += p.Precio;
        }

        decimal subtotalConDescuento = subtotal * (1 - Descuento);
        return subtotalConDescuento * (1 + impuesto);
    }

    public void Pagar()
    {
        if (_productos.Count > 0)
        {
            Estado = "Completado";
        }
    }

    public List<Producto> ObtenerProductos()
    {
        return _productos;
    }
}