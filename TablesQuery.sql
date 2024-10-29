CREATE TABLE Farmer (
    Farmer_ID INT PRIMARY KEY,
    FarmerName VARCHAR(255),
    Email VARCHAR(255),
    Password VARCHAR(255),
    CellphoneNumber VARCHAR(20),
    Location VARCHAR(255),
    Role VARCHAR(50)
);

CREATE TABLE GreenMarket (
    GreenMarket_ID INT PRIMARY KEY,
    ProductName VARCHAR(255),
    Quantity INT,
    Category VARCHAR(50),
    Price DECIMAL(10, 2),
    TransactionDate DATE,
    Farmer_ID INT,
    FOREIGN KEY (Farmer_ID) REFERENCES Farmer(Farmer_ID)
);

CREATE TABLE Product (
    Product_ID INT PRIMARY KEY,
    ProductName VARCHAR(255),
    ProductPrice DECIMAL(10, 2),
    Quantity INT,
    Category VARCHAR(50),
    ProductImage VARCHAR(255),
    Farmer_ID INT,
    FOREIGN KEY (Farmer_ID) REFERENCES Farmer(Farmer_ID)
);

CREATE TABLE Transactions (
    TransactionID INT PRIMARY KEY,
    Product_ID INT,
    ProductName VARCHAR(255),
    Quantity INT,
    BuyerName VARCHAR(255),
    BuyerLocation VARCHAR(255),
    TransactionDate DATE,
    ProductPrice DECIMAL(10, 2),
    FarmerName VARCHAR(255),
    GreenMarket_ID INT,
    FOREIGN KEY (Product_ID) REFERENCES Product(Product_ID),
    FOREIGN KEY (GreenMarket_ID) REFERENCES GreenMarket(GreenMarket_ID)
);

CREATE TABLE [User] (
    User_ID INT PRIMARY KEY,
    UserName VARCHAR(255),
    Email VARCHAR(255),
    Password VARCHAR(255),
    Role VARCHAR(50),
    PhoneNumber VARCHAR(20),
    Location VARCHAR(255)
);

CREATE TABLE EventsTB (
    Event_ID INT PRIMARY KEY,
    Event_Name VARCHAR(255),
    Category VARCHAR(50),
    Product_Price DECIMAL(10, 2),
    Farmer_ID INT,
    EventImage VARCHAR(255),
    EventDate DATE,
    EventLocation VARCHAR(255),
    FOREIGN KEY (Farmer_ID) REFERENCES Farmer(Farmer_ID)
);

CREATE TABLE EventEnrollments (
    EventEnrollmentID INT PRIMARY KEY,
    Event_ID INT,
    Employee_ID INT,
    FOREIGN KEY (Event_ID) REFERENCES EventsTB(Event_ID)
);

CREATE TABLE Employee (
    Employee_ID INT PRIMARY KEY,
    EmployeeName VARCHAR(255),
    Email VARCHAR(255),
    Password VARCHAR(255),
    CellphoneNumber VARCHAR(20),
    Role VARCHAR(50)
);

CREATE TABLE PostTB (
    PostID INT PRIMARY KEY,
    Title VARCHAR(255),
    Content TEXT,
    Category VARCHAR(50),
    CreatedBy VARCHAR(255),
    CreatedDate DATE
);

CREATE TABLE RepliesTB (
    ReplyID INT PRIMARY KEY,
    PostID INT,
    Content TEXT,
    CreatedBy VARCHAR(255),
    CreatedDate DATE,
    FOREIGN KEY (PostID) REFERENCES PostTB(PostID)
);

CREATE TABLE GrantTB (
    GrantID INT PRIMARY KEY,
    GrantName VARCHAR(255),
    GrantDescription TEXT,
    GrantAmount DECIMAL(10, 2)
);

CREATE TABLE ProjectTB (
    ProjectID INT PRIMARY KEY,
    ProjectName VARCHAR(255),
    ProjectType VARCHAR(50),
    Description TEXT,
    EnableDate DATE
);

CREATE TABLE MessageTB (
    MessageID INT PRIMARY KEY,
    SenderID INT,
    ReceiverID INT,
    MessageText TEXT,
    MessageDate DATE
);
