CREATE TABLE Usuarios (
    Cpf VARCHAR(11) PRIMARY KEY,
    Nome VARCHAR(200) NOT NULL,
    Email VARCHAR(200) NOT NULL
);

CREATE TABLE Eventos (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(200) NOT NULL,
    CapacidadeTotal INT NOT NULL,
    DataEvento TIMESTAMP NOT NULL,
    PrecoPadrao DECIMAL(10,2) NOT NULL
);

CREATE TABLE Cupons (
    Codigo VARCHAR(50) PRIMARY KEY,
    PorcentagemDesconto DECIMAL(5,2) NOT NULL,
    ValorMinimoRegra DECIMAL(10,2) NOT NULL
);

CREATE TABLE Reservas (
    Id SERIAL PRIMARY KEY,
    UsuarioCpf VARCHAR(11) NOT NULL,
    EventoId INT NOT NULL,
    CupomUtilizado VARCHAR(50),
    ValorFinalPago DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_Reservas_Usuarios
        FOREIGN KEY (UsuarioCpf)
        REFERENCES Usuarios(Cpf),

    CONSTRAINT FK_Reservas_Eventos
        FOREIGN KEY (EventoId)
        REFERENCES Eventos(Id),

    CONSTRAINT FK_Reservas_Cupons
        FOREIGN KEY (CupomUtilizado)
        REFERENCES Cupons(Codigo)
);