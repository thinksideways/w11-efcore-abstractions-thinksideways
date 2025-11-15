SET IDENTITY_INSERT Items ON;
INSERT INTO Items (Id, Name, AttackPower, Durability, Discriminator)
VALUES
    (1, 'Sword', 100, 75, 'Weapon'),
    (2, 'Axe', 100, 75, 'Weapon'),
    (3, 'Spear', 120, 55, 'Weapon');

INSERT INTO Items (Id, Name, DefenseRating, Durability, Discriminator)
VALUES
    (4, 'Shield', 85, 80, 'Armour'),
    (5, 'Helmet', 100, 80, 'Armour'),
    (6, 'Platelegs', 140, 95, 'Armour'),
    (7, 'Platebody', 160, 95, 'Armour');
SET IDENTITY_INSERT Items OFF;