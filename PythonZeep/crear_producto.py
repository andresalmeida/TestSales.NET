# import zeep
# from requests import Session
# from zeep.transports import Transport

# # Configura el cliente para deshabilitar la verificación SSL
# session = Session()
# session.verify = False  # Desactiva la verificación del certificado SSL

# # Crea el transporte con la sesión modificada
# transport = Transport(session=session)

# # Define la URL del WSDL (asegúrate de que sea la correcta)
# url = 'http://localhost:54738/ProductService.svc?wsdl'  # O cambia esto a HTTP si es necesario

# # Crea el cliente de zeep
# client = zeep.Client(wsdl=url, transport=transport)

# # Ahora puedes hacer llamadas al servicio SOAP como antes
# product = {
#     "ProductID": 5,
#     "ProductName": "Tomatodo",
#     "CategoryID": 1,
#     "UnitPrice": 5,
#     "UnitsInStock": 2
# }

# # Llama al método del servicio
# response = client.service.CreateProduct(product)
# print(response)

# CRUD ACTUALIZADO PARA QUE SEA AUTOMATICO

# import zeep
# from requests import Session
# from zeep.transports import Transport

# # Configuración de la sesión para desactivar la verificación SSL
# def get_zeep_client(wsdl_url):
#     session = Session()
#     session.verify = False  # Desactiva la verificación SSL
#     transport = Transport(session=session)
#     return zeep.Client(wsdl=wsdl_url, transport=transport)

# # Función para crear un producto
# def create_product(client, product_data):
#     try:
#         response = client.service.CreateProduct(product_data)
#         print("Producto creado:", response)
#     except Exception as e:
#         print(f"Error creando producto: {e}")

# # Función para obtener un producto por ID
# def get_product_by_id(client, product_id):
#     try:
#         response = client.service.RetrieveProductById(product_id)
#         print(f"Producto obtenido: {response}")
#     except Exception as e:
#         print(f"Error obteniendo producto: {e}")

# # Función para actualizar un producto
# def update_product(client, product_data):
#     try:
#         response = client.service.UpdateProduct(product_data)
#         print("Producto actualizado:", response)
#     except Exception as e:
#         print(f"Error actualizando producto: {e}")

# # Función para eliminar un producto
# def delete_product(client, product_id):
#     try:
#         response = client.service.DeleteProduct(product_id)
#         print("Producto eliminado:", response)
#     except Exception as e:
#         print(f"Error eliminando producto: {e}")

# # Función para automatizar las operaciones de productos
# def manage_products(wsdl_url, operation, data=None):
#     client = get_zeep_client(wsdl_url)
    
#     if operation == 'create':
#         create_product(client, data)
#     elif operation == 'get':
#         get_product_by_id(client, data)
#     elif operation == 'update':
#         update_product(client, data)
#     elif operation == 'delete':
#         delete_product(client, data)
#     else:
#         print(f"Operación {operation} no válida.")

# # Ejemplo de cómo usar las funciones automatizadas

# # URL del WSDL del servicio SOAP
# wsdl_url = 'http://localhost:54738/ProductService.svc?wsdl'  # Usa 'http' si no es HTTPS

# # Datos del producto a crear
# product_data = {
#     "ProductID": 11,
#     "ProductName": "MIC",
#     "CategoryID": 1,
#     "UnitPrice": 35.99,
#     "UnitsInStock": 12
# }

# # Llamar a las funciones automatizadas
# manage_products(wsdl_url, 'create', product_data)  # Crear producto
# manage_products(wsdl_url, 'get', 1)  # Obtener producto con ID 1
# manage_products(wsdl_url, 'update', {
#     "ProductID": 1,
#     "ProductName": "Producto Actualizado Automático",
#     "CategoryID": 2,
#     "UnitPrice": 15.99,
#     "UnitsInStock": 80
# })  # Actualizar producto
# manage_products(wsdl_url, 'delete', 1)  # Eliminar producto con ID 1

# CODIGO PARA VER LOS METODOS DISPONIBLES

# from zeep import Client
# client = Client('http://localhost:54738/ProductService.svc?wsdl')
# print(client.wsdl.dump())

# CODIGO PARA EL GET

import zeep
from requests import Session
from zeep.transports import Transport
from zeep.exceptions import Fault

def get_zeep_client(wsdl_url):
    session = Session()
    session.verify = False
    transport = Transport(session=session, timeout=60)
    return zeep.Client(wsdl=wsdl_url, transport=transport)

def retrieve_all_products(client):
    try:
        response = client.service.RetrieveAllProducts()
        if response:
            print("Lista de Productos:")
            for product in response:
                print(
                    f"ID: {product['ProductID']}, "
                    f"Nombre: {product['ProductName']}, "
                    f"Categoría: {product['CategoryID']}, "
                    f"Precio: {product['UnitPrice']}, "
                    f"Stock: {product['UnitsInStock']}"
                )
        else:
            print("No se encontraron productos.")
    except Fault as fault:
        print(f"Error en la operación SOAP: {fault}")
    except Exception as e:
        print(f"Error inesperado: {e}")

# URL del servicio SOAP
wsdl_url = "http://localhost:54738/ProductService.svc?wsdl"
client = get_zeep_client(wsdl_url)

print("\n----- Lista de Productos -----")
retrieve_all_products(client)

