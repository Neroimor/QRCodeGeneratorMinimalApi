
# 📦 QR Code Generator API (.NET 9)

Простое веб-приложение на ASP.NET Core для генерации QR-кодов по ссылке или произвольному тексту.

## 🚀 Как использовать

### 🔗 Получение справки

**GET** `/help`

Возвращает краткую справку о том, как пользоваться API.

Пример:

```
GET https://localhost:7138/help
```

Ответ:

```
Для получения QR-кода введите в URL браузера, Postman или на frontend адрес такого формата:
https://localhost:7138/qr?content=https://www.youtube.com/watch?v=dQw4w9WgXcQ

Где:
- https://localhost:7138/qr — конечная точка
- content — query-параметр, содержащий текст или ссылку, которую вы хотите закодировать
```

---

### 📷 Генерация QR-кода

**GET** `/qr`

**Параметры запроса:**

|Параметр|Тип|Обязательный|Описание|
|---|---|---|---|
|content|string|✅ Да|Текст или URL, который нужно закодировать в QR-код|

Пример:

```
GET https://localhost:7138/qr?content=https://www.youtube.com/watch?v=dQw4w9WgXcQ
```

📥 **Ответ:** PNG-файл с QR-кодом, который откроется в браузере или скачивается, в зависимости от настроек браузера.

---

## ⚙️ Технологии

- ASP.NET Core (.NET 9)
- ZXing.Net
- ImageSharp (`SixLabors.ImageSharp`)
- Dependency Injection (через интерфейс `IQRGenerator`)

---

## 🛠 Пример зависимости

Убедитесь, что в `Program.cs` зарегистрирован `IQRGenerator`:

```csharp
builder.Services.AddScoped<IQRGenerator, QRGenerate>();
```

---

## 🧪 Примеры использования

🧑‍💻 В браузере:

```
https://localhost:7138/qr?content=HelloWorld
```

🧰 В Postman:

- Метод: `GET`
    
- URL: `https://localhost:7138/qr?content=AnyTextHere`


![qr](https://github.com/user-attachments/assets/6965da5b-0f1f-4f78-9cff-aa5a81aad3e8)




