using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
      
        List<Producto> inventario = new List<Producto>
        {
            new Producto("Teclado Mecánico", 15000m, 5),
            new Producto("Mouse Gamer", 8000m, 3),
            new Producto("Monitor 24''", 45000m, 2)
        };

        Pedido pedidoActual = new Pedido();
        bool ejecutando = true;

        while (ejecutando)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   SISTEMA DE GESTIÓN DE PEDIDOS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Ver Inventario y Agregar Producto");
            Console.WriteLine("2. Ver Resumen del Pedido");
            Console.WriteLine("3. Pagar y Finalizar Pedido");
            Console.WriteLine("4. Salir");
            Console.WriteLine("----------------------------------------");
            Console.Write("Seleccione una opción: ");

           
            if (!int.TryParse(Console.ReadLine(), out int opcion))
            {
                Console.WriteLine("\nOpción inválida. Ingrese un número.");
                PresioneParaContinuar();
                continue;
            }

            switch (opcion)
            {
                case 1:
                    MostrarInventario(inventario, pedidoActual);
                    break;
                case 2:
                    MostrarResumen(pedidoActual);
                    break;
                case 3:
                    RealizarPago(pedidoActual);
                    break;
                case 4:
                    ejecutando = false;
                    Console.WriteLine("\n¡Gracias por utilizar el sistema!");
                    break;
                default:
                    Console.WriteLine("\nOpción fuera de rango.");
                    PresioneParaContinuar();
                    break;
            }
        }
    }

   
    static void MostrarInventario(List<Producto> inventario, Pedido pedido)
    {
        Console.Clear();
        Console.WriteLine("=== INVENTARIO DE PRODUCTOS ===");
        for (int i = 0; i < inventario.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {inventario[i].Nombre} - Precio: ${inventario[i].Precio:F2} - Stock: {inventario[i].Stock}");
        }
        Console.WriteLine("--------------------------------");

        Console.Write("Seleccione el número de producto a comprar (0 para volver): ");
        if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= inventario.Count)
        {
           
            Producto prodSeleccionado = inventario[seleccion - 1];

            Console.Write($"Ingrese la cantidad de '{prodSeleccionado.Nombre}' a llevar: ");
            if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
            {
                
                if (pedido.AgregarProducto(prodSeleccionado, cantidad))
                {
                    Console.WriteLine($"\n✓ ¡{cantidad} unidad(es) agregada(s) con éxito al pedido!");
                }
                else
                {
                    Console.WriteLine($"\n✗ ERROR: Stock insuficiente. Solo quedan {prodSeleccionado.Stock} unidades disponibles.");
                }
            }
            else
            {
                Console.WriteLine("\nCantidad no válida.");
            }
        }
        PresioneParaContinuar();
    }

    static void MostrarResumen(Pedido pedido)
    {
        Console.Clear();
        Console.WriteLine("=== RESUMEN DEL PEDIDO ===");
        Console.WriteLine($"Estado: {pedido.Estado}");
        Console.WriteLine("--------------------------------");

        List<Producto> prods = pedido.ObtenerProductos();
        if (prods.Count == 0)
        {
            Console.WriteLine("El carrito está vacío.");
        }
        else
        {
            foreach (Producto p in prods)
            {
                Console.WriteLine($"- {p.Nombre}: ${p.Precio:F2}");
            }

            decimal impuestoIVA = 0.21m; // 21% IVA
            decimal total = pedido.CalcularTotal(impuestoIVA);

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Total con IVA (21%): ${total:F2}");
        }
        PresioneParaContinuar();
    }

    static void RealizarPago(Pedido pedido)
    {
        Console.Clear();
        Console.WriteLine("=== PROCESAR PAGO ===");
        if (pedido.ObtenerProductos().Count == 0)
        {
            Console.WriteLine("No hay productos en el pedido para abonar.");
        }
        else if (pedido.Estado == "Completado")
        {
            Console.WriteLine("Este pedido ya fue pagado previamente.");
        }
        else
        {
            pedido.Pagar();
            Console.WriteLine($"\n✓ ¡Pago realizado con éxito! El estado del pedido ahora es: '{pedido.Estado}'.");
        }
        PresioneParaContinuar();
    }

    static void PresioneParaContinuar()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}