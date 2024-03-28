![image](https://github.com/Saritik/Corona_system/assets/116844169/e7c1fddd-f309-4728-a048-3872c5532ac9)# Corona_system
Corona virus management system for Health Insurance Fund

needs to do the migrations to open the sql tables:
1. 'Add-Migration InitialCreate'
2. 'Update-Database'

How to use the system:

Upon entering the system, users are directed to the home page where they can choose between two options: 
viewing a list of members registered in the health insurance fund or 
accessing a graph that illustrates the daily count of sick individuals over the past month.
![Upon entering the system, users are directed to the home page where they can choose between two options: 
viewing a list of members registered in the health insurance fund or 
accessing a graph that illustrates the daily count of sick individuals over the past month.](https://github.com/Saritik/Corona_system/assets/116844169/e7c1fddd-f309-4728-a048-3872c5532ac9)

From there we can navigate to the list of members registered in the system
![From there we can navigate to the list of members registered in the system](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20145850.png)

After entering the list, we will try to add a new HMO member to the list
![After entering the list, we will try to add a new HMO member to the list](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20150245.png)
![After entering the list, we will try to add a new HMO member to the list](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20150306.png)

The list is updated after the addition to the list
![The list is updated after the addition to the list](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20150334.png)

We will edit the new registrant and add the vaccinations he needs to perform and we will see the actions on the vaccinations
![We will edit the new registrant and add the vaccinations he needs to perform and we will see the actions on the vaccinations](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20150503.png)
![We will edit the new registrant and add the vaccinations he needs to perform and we will see the actions on the vaccinations](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20150503.png)

His vaccination list is currently empty
![His vaccination list is currently empty](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20151121.png)

We will add a vaccine to the list of vaccines
![We will add a vaccine to the list of vaccines ](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20151217.png)

And our table has been updated and shows us the additional vaccine
![And our table has been updated and shows us the additional vaccine](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20151250.png)

We can edit the vaccine and see the update afterwards in the table (we changed the manufacturer's name)
![We can edit the vaccine and see the update afterwards in the table (we changed the manufacturer's name)](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20151552.png)
![We can edit the vaccine and see the update afterwards in the table (we changed the manufacturer's name)](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20151724.png)

We can view the vaccine details by clicking on the details button
![We can view the vaccine details by clicking on the details button](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20151746.png)

We will try to create another vaccine whose date will take place before the existing vaccine and we can see that the system will not allow this 
because there is an order for receiving the vaccines
![We will try to create another vaccine whose date will take place before the existing vaccine and we can see that the system will not allow this 
because there is an order for receiving the vaccines](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20151838.png)

We added 3 more vaccines so that we reached 4 vaccines for the same member
![We added 3 more vaccines so that we reached 4 vaccines for the same member ](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152325.png)

Now we will try to add another vaccine to him and we can see that the system does not approve it because up to 4 vaccines are allowed for each member of the health fund
![Now we will try to add another vaccine to him and we can see that the system does not approve it because up to 4 vaccines are allowed for each member of the health fund](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152403.png)

There is also an option to delete a vaccine from the list of vaccines (this is all part of the option to edit a health insurance member)
![There is also an option to delete a vaccine from the list of vaccines (this is all part of the option to edit a health insurance member)](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152531.png)

The list of vaccinations after deletion
![The list of vaccinations after deletion](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152605.png)

After returning to the window of updating a member at the health fund and performing the update, 
we can return to view the details of that member and we will also see the list of vaccines added to him
![After returning to the window of updating a member at the health fund and performing the update, 
we can return to view the details of that member and we will also see the list of vaccines added to him](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152737.png)

We can also delete a member in the list of members
![We can also delete a member in the list of members](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152804.png)

The updated list after the deletion
![The updated list after the deletion](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152818.png)

We'll go back to the home page and this time we'll go to the graph page to see how many members of the fund 
are not vaccinated and how many were sick every day in the last month
![We'll go back to the home page and this time we'll go to the graph page to see how many members of the fund 
are not vaccinated and how many were sick every day in the last month](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152924.png)
![We'll go back to the home page and this time we'll go to the graph page to see how many members of the fund 
are not vaccinated and how many were sick every day in the last month](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20152947.png)

![In addition, at the bottom of each page there is a link that leads to the health insurance policy page](https://github.com/Saritik/Corona_system/blob/main/%D7%A6%D7%99%D7%9C%D7%95%D7%9D%20%D7%9E%D7%A1%D7%9A%202024-03-28%20153009.png)
