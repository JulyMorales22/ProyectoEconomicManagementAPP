# ProyectoEconomicManagementAPP
#Funcionamiento App:

Esta APP fue diseñada para que lleves un control de tus finanzas,puedes crear tipos de cuentas y cuentas a esos tipos de cuentas, puedes registrar categorías dependiendo el tipo de operación que sea, un ingreso o egreso y así poder registrar tus transacciones diarias de manera fácil y detallada.


#Indicaciones:
En la pestaña Home podrás ver los tipos de cuentas que tienes registrado y crear nuevas, al desplegarla podrás encontrar las cuentas que tienes asociadas y con los botones modificar y eliminar que se encuentran al frente de cada cuenta podras hacer dichas opciones y tenemos tambien los botones de eliminar y modifcar tipos de cuentas. En la pestaña categoríes, tendrás acceso a crear según el tipo de operación que escogas (ingresos, egresos que ya estan definidas en la DB), modificar y eliminar categorías.Tambien podras listar todas las transacciones realizadas, con los iconos podras eliminar y modificar tu informacion como usuario y al regresar al Home podrás nuevamente buscar la cuenta desde la cuál quieres crear una transacción llenar los datos y listo así de fácil podrás empezar a registrar tus movimientos diarios y llevar un control de tus finanzas personales. 

#Las entidades que tienen FK tienen un campo adicional llamado DbStatus, para a la hora de eliminar identificar si se debe eliminar definitivamente o solo cambiar el estado en la DB ya que tiene movimientos en su FK

#En la carpeta del proyecto esta el archivo EconomicManagementDB con el script para ejecutar la base de datos 


#Vista Login

 ![image](https://user-images.githubusercontent.com/90154419/161608867-f6ea662c-043e-454e-bb94-fb7e7f34bee7.png)

#Vista Lista Account Types

![image](https://user-images.githubusercontent.com/90154419/161608900-d1575cd0-6044-4281-8d34-144440a4a6e7.png)

#Vista Account relacionadas con Account Types

![image](https://user-images.githubusercontent.com/90154419/161608939-4e539de9-476f-4cd6-94f4-a24842442cb0.png)

#Vista Category

![image](https://user-images.githubusercontent.com/90154419/161608981-6f180258-e028-486e-9e42-0228e3eb7fc7.png)

#Vista transactions

![image](https://user-images.githubusercontent.com/90154419/161609031-3b0129bb-af94-484a-9558-e1d6be44a7bc.png)


#Vista Create User

![image](https://user-images.githubusercontent.com/90154419/161609073-bef26937-9300-4dec-aa57-7ca999579b31.png)







