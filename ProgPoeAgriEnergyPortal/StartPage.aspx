<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.StartPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AgriEnergy Portal</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <!-- Custom CSS -->
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 1000px;
            margin: 0 auto;
            padding: 20px;
        }
        .header, .content, .footer {
            text-align: center;
            margin-bottom: 20px;
        }
        .header h1 {
            font-size: 2.5em;
            color: #343a40;
        }
        .content p {
            font-size: 1.2em;
            color: #6c757d;
        }
        .carousel-inner img {
            width: 100%;
            height: 500px;
            object-fit: cover;
        }
        .btn-login {
            background-color: #28a745;
            color: white;
            padding: 10px 20px;
            font-size: 1.2em;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s;
        }
        .btn-login:hover {
            background-color: #218838;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header Section -->
            <div class="header">
                <h1>Welcome to AgriEnergy Portal</h1>
            </div>
            
            <!-- Carousel Section -->
            <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel" data-interval="10000">
                <ol class="carousel-indicators">
                    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                </ol>
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="https://www.hashmicro.com/blog/wp-content/uploads/2022/09/Gautam-Adani-IRMA-Speech.jpg" class="d-block w-100" alt="Farming"/>
                    </div>
                    <div class="carousel-item">
                        <img src="https://eu-images.contentstack.com/v3/assets/blt4175b16074920322/blt7eda3d7553370277/638f66caa5f64404871a6e67/9-20-21_20beef_20cattle_201.jpg?disable=upscale&width=1200&height=630&fit=crop" class="d-block w-100" alt="Cattles"/>
                    </div>
                    <div class="carousel-item">
                        <img src="https://miro.medium.com/v2/resize:fit:1358/0*dtaXle5NAESOua2O" class="d-block w-100" alt="Green energy"/>
                    </div>
                </div>
                <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>

            <!-- Content Section -->
            <div class="content">
                <p>AgriEnergy Portal is your one-stop solution for all agricultural and energy needs. Explore our vast range of products and services tailored to meet the modern farmer's requirements.</p>
                <p>Join us to experience the best in agricultural innovation and sustainable energy solutions.</p>
            </div>

            <!-- Button Section -->
            <div>
                <asp:Button ID="btnLogin" runat="server" CssClass="btn-login" Text="Login to portal" PostBackUrl="~/LoginPage.aspx"/>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>
</body>
</html>
