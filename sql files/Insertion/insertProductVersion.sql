use AKADEMEDYA

insert into PRODUCT_VERSION(VersionID, ProductID, PVDescription) values
(
    1.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 1.0'
),
(
    1.2,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 1.2'
),
(
    1.24,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 1.24'
),
(
    1.8,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 1.8'
),
(
    2.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 2.0'
),
(
    2.72,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 2.72'
),
(
    2.8,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 2.8'
),
(
    3.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 3.0'
),
(
    3.2,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 3.2'
),
(
    4.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC'
    ),
    'Version 4.0'
),
(
    2.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 1.0'
),
(
    2.34,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 2.34'
),
(
    2.44,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 2.44'
),
(
    3.11,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 3.11'
),
(
    3.88,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 3.88'
),
(
    4.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 4.0'
),
(
    4.55,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 4.55'
),
(
    5.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 5.0'
),
(
    6.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 6.0'
),
(
    6.1,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'ISGPRO'
    ),
    'Version 6.1'
),
(
    1.01,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 1.0'
),
(
    2.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 2.34'
),
(
    3.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 2.44'
),
(
    3.32,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 3.11'
),
(
    4.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 3.88'
),
(
    4.44,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 4.0'
),
(
    4.90,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 4.55'
),
(
    5.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 5.0'
),
(
    5.25,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 6.0'
),
(
    5.8,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'IRONIC-OSGB'
    ),
    'Version 6.1'
),
(
    1.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 1.0'
),
(
    2.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 2.34'
),
(
    3.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 2.44'
),
(
    3.70,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 3.11'
),
(
    4.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 3.88'
),
(
    4.20,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 4.0'
),
(
    4.85,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 4.55'
),
(
    5.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 5.0'
),
(
    6.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 6.0'
),
(
    7.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'MOBILISG'
    ),
    'Version 6.1'
),
(
    1.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 1.0'
),
(
    2.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 2.34'
),
(
    3.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 2.44'
),
(
    4.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 3.11'
),
(
    5.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 3.88'
),
(
    6.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 4.0'
),
(
    7.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 4.55'
),
(
    8.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 5.0'
),
(
    9.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 6.0'
),
(
    10.0,
    (
        SELECT p.ProductID
        FROM PRODUCT_ p
        WHERE p.PName = 'AYS'
    ),
    'Version 6.1'
);
