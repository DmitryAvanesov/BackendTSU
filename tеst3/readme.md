# Контрольная 3: вариант 2
## Задача
1. Разработать API используя PHP в качестве серверного языка.  
### Спецификации API 
Корень API должен располагаться в ```/api```. 
Все запросы, за исключением Login, Register должны содержать хэдер ```Authorization: Bearer <token>```. 

**Доступные ресурсы**: 
 
* ```POST: api/login```: Принимает:

```js
{
    login: <login>,
    password: <password>
}
``` 
возвращает ```{token: <token>}``` в случае успеха, или текст ошибки с кодом 401 в случае неудачи (HTTP/1.0 401 Unauthorized).
* ```POST: api/register```: Регистрация. Принимает:
 ```js 
{
    login: <login>,
    password: <password>
}
``` 
возвращает ```{token: <token>}``` в случае успеха, или текст ошибки с кодом 401 в случае неудачи (HTTP/1.0 401 Unauthorized).Нельзя создать двух пользователей с одним логином. 
* ```POST: api/logout``` - удаляет токен из бд. Возвращает результат операции. 
У вас должны быть созданы пользователи: 
  * Admin:admin. Роль: Админ
  * User:user. Роль: Просто пользователь.
* ```GET: api/users```: Список пользователей. Возвращает: 
```js
    {
        list: [
            {
                id: 1, login: "login", role: "admin" 
            }, {
                id: 2, login: "login", role: "regular"
            }]
    }
```
Доступно любому пользователю. Иначе возвращает (HTTP/1.0 401 Unauthorized).
* ```POST: api/users``` Создание пользователя. Принимает: 
```js
{
    login: <login>, 
    password: <password>,
    role: <user role: admin or regular>
} 
```
Доступно только админам. Иначе возвращает (HTTP/1.0 403 Access Denied)
* ```DELETE: api/users/<id>``` Deletes the user. Returns status of operation. 

Доступно только админам. Иначе возвращает (HTTP/1.0 403 Access Denied).
 
Если такого пользователя нет то (HTTP/1.0 404 Not Found).



* ```GET: api/teachers```: Список преподавателей. Возвращает: 
```js
    {//Пример шаблона, возвращайте данные из БД!
        list: [
            {
                id: 1, 
                fullName: "Соколов Данила Александрович",
                birthday: "30.04.1992",
                avatar: "URL",                  
            }, {
                //...
            }]
    }
```
Доступно любому пользователю. 
* ```POST: api/teachers``` Создание преподавателя. Получает: 
```js
{
    fullName: "Соколов Данила Александрович",
    birthday: "30.04.1992",
    avatar: "URL"                    
}
```
Доступно только админам. Простые пользователи получают (HTTP/1.0 403 Access Denied), неавторизованные (HTTP/1.0 401 Unauthorized).
* ```GET: api/teachers/<id>``` Информация о конкретном преподавателе. Возвращает: 
```js
    {//Пример шаблона, возвращайте данные из БД!
        list: [
            {
                id: 1, 
                fullName: "Соколов Данила Александрович",
                birthday: "30.04.1992",
                avatar: "URL",
                subjects: 
                [
                    {
                        id: 1, 
                        name: "Subj 1",         
                    }, 
                    { //... }
                ] 
                  
            }, {
                //...
            }]
    }
```
Доступно любому залогиненному пользователю. Остальные получают (HTTP/1.0 401 Unauthorized).

Если такого преподавателя нет, возвращает (HTTP/1.0 404 Not Found).
* ```DELETE: api/teachers/<id>``` Удаляет преподавателя, возвращает статус операции. 

Доступно только админам. Простые пользователи получают (HTTP/1.0 403 Access Denied), неавторизованные (HTTP/1.0 401 Unauthorized).

Если такого преподавателя нет, возвращает (HTTP/1.0 404 Not Found).
 
* ```GET: api/subjects``` Список всех предметов, возвращает:

```js
{ //Пример шаблона, возвращайте данные из БД!
    list: [
        {
            id: 1, 
            name: "Subj 1", 
            teacher: 1            
        }, 
        { //... }
    ]
}
```

Доступно всем.
* ```POST: api/subjects``` Создание предмета, принимает:

```js
{
    name: "Subj 1", 
    schedule: 
    [
        {
            day: 1, 
            time: "10:35"
        },
        {
            day: 5, 
            time: "12:25"
        }   
    ], 
    teacher: 1            
}
```
Доступно только админам. Простые пользователи получают (HTTP/1.0 403 Access Denied), неавторизованные (HTTP/1.0 401 Unauthorized).

Если такого преподавателя нет, возвращает (HTTP/1.0 400 Bad Request).
* ```GET: api/subjects/<id>``` Информация о конкретном предмете, возвращает: 

```js
{
    id: 1, 
    name: "Subj 1", 
    schedule: 
    [
        {
            day: 1, 
            time: "10:35"
        },
        {
            day: 5, 
            time: "12:25"
        }   
    ], 
    teacher: 1            
}
```
Если такого предмета нет, возвращает (HTTP/1.0 404 Not Found).
Доступно всем.
* ```DELETE: api/subjects/<id>``` Удаление поста.

Доступно только админам. Простые пользователи получают (HTTP/1.0 403 Access Denied), неавторизованные (HTTP/1.0 401 Unauthorized).

Если такого предмета нет, возвращает (HTTP/1.0 404 Not Found).
* ```PUT: api/subjetcs/<id>``` Обновление существующего предмета, получает на вход те же данные, что и POST

Доступно только админам. Простые пользователи получают (HTTP/1.0 403 Access Denied), неавторизованные (HTTP/1.0 401 Unauthorized).

Если такого поста нет, возвращает (HTTP/1.0 404 Not Found).



### Как сдать задачу
Создайте файл credentials.md в вашем репозитории с: 
1. Запушить свой код в этот репозиторий.
2. Создать дамп БД (экспорт в phpMyAdmin) и также поместить ег ов этот репозиторий. 
3. Создать файл instructions.md в котором вы должны описать все логины-пароли для проверки (для пользователей), а также можете указать инструкции по проверке. 


