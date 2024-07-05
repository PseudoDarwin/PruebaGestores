Create Database BDGestoresPrueba

Use BDGestoresPrueba

CREATE TABLE Gestor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50)
);

CREATE TABLE Saldo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Monto DECIMAL(10, 2)
);

CREATE TABLE Asignacion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    GestorId INT,
    SaldoId INT,
    FOREIGN KEY (GestorId) REFERENCES Gestor(Id),
    FOREIGN KEY (SaldoId) REFERENCES Saldo(Id)
);


-- Crear Gestores
INSERT INTO Gestor (Nombre)
VALUES ('Juan'), ('Pedro'), ('Camilo'), ('Manuel'), ('Carrion'),
       ('José'), ('Alberto'), ('Agustin'), ('Wos'), ('Darwin');

-- Crear Saldos
INSERT INTO Saldo (Monto)
VALUES (2277), (3953), (4726), (1414), (627), (1784), (1634), (3958), (2156), (1347),
       (2166), (820), (2325), (3613), (2389), (4130), (2007), (3027), (2591), (3940),
       (3888), (2975), (4470), (2291), (3393), (3588), (3286), (2293), (4353), (3315),
       (4900), (794), (4424), (4505), (2643), (2217), (4193), (2893), (4120), (3352),
       (2355), (3219), (3064), (4893), (272), (1299), (4725), (1900), (4927), (4011);





CREATE PROCEDURE PAAsignarSaldos
AS
BEGIN
    DECLARE @TotalGestores INT = (SELECT COUNT(*) FROM Gestor);
    DECLARE @GestorActual INT = 1;

    --Ordemaos los saldos de mayor a menor
    DECLARE SaldoCursor CURSOR FOR
    SELECT Id FROM Saldo ORDER BY Monto DESC;

    OPEN SaldoCursor;
    DECLARE @SaldoId INT;

    FETCH NEXT FROM SaldoCursor INTO @SaldoId;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Asignamos el saldo actual al gestor actual
        INSERT INTO Asignacion (GestorId, SaldoId)
        VALUES (@GestorActual, @SaldoId);

        -- Avanzamos con el puntero al siguiente gestor
        SET @GestorActual = @GestorActual + 1;
        IF @GestorActual > @TotalGestores
        BEGIN
            SET @GestorActual = 1;
        END

        FETCH NEXT FROM SaldoCursor INTO @SaldoId;
    END

    CLOSE SaldoCursor;
    DEALLOCATE SaldoCursor;
END;





create view ConsultarMontos as 
SELECT
    g.Nombre,
    STRING_AGG(CAST(s.Monto AS VARCHAR), ', ') AS Montos
FROM
    Asignacion a
JOIN
    Gestor g ON a.GestorId = g.Id
JOIN
    Saldo s ON a.SaldoId = s.Id
GROUP BY
    g.Nombre

Select * from Gestor
Select * from Saldo
SELECT * FROM Saldo ORDER BY Monto DESC;
