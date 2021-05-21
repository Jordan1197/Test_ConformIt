CREATE TABLE evenement (
	id SERIAL PRIMARY KEY,
	titre VARCHAR(100),
	description VARCHAR(255),
	personneresponsable VARCHAR(50),
	listecommentaire text[][]
);

CREATE TABLE commentaire (
	id SERIAL PRIMARY KEY,
	evenementid INT,
	description VARCHAR(255),
	date DATE,
	FOREIGN KEY (evenementid)
	REFERENCES evenement (id)
)