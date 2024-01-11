//importar o módulo supertest
const request = require('supertest');

//importar o main app file
const app = require('../../src/app');

//criar uma variável que guarda a rota do endpoint
const route = '/reservations';

//Variáveis para armazenamento de user e restaurante na função beforeAll
let user;
let restaurant;

//Função beforeAll para criação de items em todas as tabelas das quais depende a tabela das reservas
beforeAll(async () => {

  //Registar um user na tabela registerusers
  const registerUser = await app.db('registerusers').insert({
    username: 'joaorodrigues',
    email: 'moreira@gmail.com',
    password: '55555'
  }, '*');

  //Registar um user na tabela users
  const createUser = await app.db('users').insert({
    username: 'joaorodrigues',
    email: 'moreira@gmail.com',
    password: '55555',
    registeruser_id: registerUser[0].id
    } , '*');
  user = { ...createUser[0] };

  //Registar um restaurante na tabela restaurantregistrations
  const registerRestaurant = await app.db('restaurantregistrations').insert({
    flname: 'joao moreira',
    phone: 999888888,
    email: 'moreira55@gmail.com',
    password: '4444',
    name: 'la piola',
    category: 'comida italiana',
    desc: 'pizza pesto pasta',
    rphone: 253253253,
    location: 'braga',
    image: 'image',
    numberoftables: 20,
    capacity: 100,
    openingdays: 'segunda-sexta',
    averageprice: 17,
    openinghours: '12:00',
    closinghours: '23:00'
  }, '*');
  
  //Registar um responsável de um restaurante na tabela restaurantresponsibles
  const registerResponsible = await app.db('restaurantresponsibles').insert({
    flname: 'joao moreira',
    phone: 999888888,
    email: 'moreira55@gmail.com',
    password: '4444',
    restaurantregistration_id: registerRestaurant[0].id
    }, '*');
    
  //Registar um restaurante na tabela restaurants
  const createRestaurant = await app.db('restaurants').insert({
    name: 'la piola',
    category: 'comida italiana',
    desc: 'pizza pesto pasta',
    rphone: 253253253,
    location: 'braga',
    image: 'image',
    numberoftables: 20,
    capacity: 100,
    openingdays: 'segunda-sexta',
    averageprice: 17,
    openinghours: '12:00',
    closinghours: '23:00',
    restaurantregistration_id: registerRestaurant[0].id,
    rresponsible_id: registerResponsible[0].id
  }, '*');
  restaurant = { ...createRestaurant[0] };
});

//Teste para Inserir Reservas
test('Test #JR1 - Inserir Reservas', () => {
  return request(app)
    .post(route)
    .send({
      client_name: 'Joao Rodrigues JR1',
      phonenumber: 911111111,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 4,
      restaurant_id: restaurant.id,
      user_id: user.id
    })
    .then((res) => {
      expect(res.status).toBe(201);
      expect(res.body.client_name).toBe('Joao Rodrigues JR1');
    });
});

//Teste para Inserir Reservas sem Nome do Cliente
test('Test #JR2 - Inserir Reservas sem Nome do Cliente', () => {
  return request(app)
    .post(route)
    .send({
      phonenumber: 911111112,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 4,
      restaurant_id: restaurant.id,
      user_id: user.id
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Primeiro e último nome é um atributo obrigatório!');
      expect(res.body).toEqual({ error: 'Primeiro e último nome é um atributo obrigatório!' });
    });
});

//Teste para Inserir Reservas sem Número de Telefone
test('Test #JR3 - Inserir Reservas sem Número de Telefone ', () => {
  return request(app)
    .post(route)
    .send({
      client_name: 'Joao Rodrigues JR3',
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 4,
      restaurant_id: restaurant.id,
      user_id: user.id
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Numero de telefone é um atributo obrigatório!');
    });
});

//Teste para Inserir Reservas sem Data de Reserva
test('Test #JR4 - Inserir Reservas sem Data de Reserva ', () => {
  return request(app)
    .post(route)
    .send({
      client_name: 'Joao Rodrigues JR4',
      phonenumber: 911111112,
      reservationtime: '20:30:00',
      numberpeople: 4,
      restaurant_id: restaurant.id,
      user_id: user.id
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Data é um atributo obrigatório!');
    });
});

//Teste para Inserir Reservas sem Hora de Reserva
test('Test #JR5 - Inserir Reservas sem Hora de Reserva ', () => {
  return request(app)
    .post(route)
    .send({
      client_name: 'Joao Rodrigues JR5',
      phonenumber: 911111112,
      reservationdate: '2023-12-15',
      numberpeople: 4,
      restaurant_id: restaurant.id,
      user_id: user.id
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Hora é um atributo obrigatório!');
    });
});

//Teste para Inserir Reservas sem Numero de Pessoas
test('Test #JR6 - Inserir Reservas sem Numero de Pessoas ', () => {
  return request(app)
    .post(route)
    .send({
      client_name: 'Joao Rodrigues JR6',
      phonenumber: 911111112,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      restaurant_id: restaurant.id,
      user_id: user.id
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Numero de pessoas é um atributo obrigatório!');
    });
});

//Teste para Inserir Reservas sem ID de Restaurante
test('Test #JR7 - Inserir Reservas sem ID de Restaurante ', () => {
  return request(app)
    .post(route)
    .send({
      client_name: 'Joao Rodrigues JR7',
      phonenumber: 911111112,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 4,
      user_id: user.id
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('ID do restaurante é um atributo obrigatório!');
    });
});

//Teste para Inserir Reservas sem ID de Utilizador
test('Test #JR8 - Inserir Reservas sem ID de Utilizador ', () => {
  return request(app)
    .post(route)
    .send({
      client_name: 'Joao Rodrigues JR8',
      phonenumber: 911111112,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 4,
      restaurant_id: restaurant.id
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('ID do utilizador é um atributo obrigatório!');
    });
});

//Teste para Listar Reservas
test('Test #JR9 - Listar Reservas', () => {
  return app.db('reservations')
    .insert({
      client_name: 'Joao Rodrigues JR9',
      phonenumber: 911111113,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 4,
      restaurant_id: restaurant.id,
      user_id: user.id
    })
    .then(() => request(app).get(route))
    .then((res) =>{
      expect(res.status).toBe(200);
      expect(res.body.length).toBeGreaterThan(0);
    });
});

//Teste para Listar Reservas por ID
test('Test #JR10 - Listar Reservas por ID', () => {
  return app.db('reservations')
    .insert({
      client_name: 'Joao Rodrigues JR10',
      phonenumber: 911111114,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 7,
      restaurant_id: restaurant.id,
      user_id: user.id
    }, ['id'])
    .then((reserv) => request(app).get(`${route}/${reserv[0].id}`))
    .then((res) =>{
      expect(res.status).toBe(200);
      expect(res.body.client_name).toBe('Joao Rodrigues JR10');
    });
});

//Teste para Eliminar uma Resereva 
test('Test #JR11 - Eliminar uma Reserva', () => {
  let reservationId;
  return app.db('reservations')
    .insert({
      client_name: 'Joao Rodrigues JR11',
      phonenumber: 911111114,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 7,
      restaurant_id: restaurant.id,
      user_id: user.id
    }, ['id'])
    .then((reserv) => {
      reservationId = reserv[0].id;
      return request(app).delete(`${route}/${reservationId}`);
    })
    .then((res) => {
      expect(res.status).toBe(204);
      return app.db('reservations').where('id', reservationId).first();
    })
    .then((deletedReservation) => {
      expect(deletedReservation).toBeFalsy();
    });
});

//Teste para Modificar uma Reserva por ID
test('Test #JR12 - Modificar uma Reservas por ID', () => {
  return app.db('reservations')
    .insert({
      client_name: 'Joao Rodrigues JR12',
      phonenumber: 911111114,
      reservationdate: '2023-12-15',
      reservationtime: '20:30:00',
      numberpeople: 7,
      restaurant_id: restaurant.id,
      user_id: user.id
    }, ['id'])
    .then((reserv) => request(app).put(`${route}/${reserv[0].id}`)
      .send({numberpeople: 8})
      .then((res) =>{
        expect(res.status).toBe(200);
        expect(res.body.client_name).toBe('Joao Rodrigues JR12');
        expect(res.body.numberpeople).toBe(8);
    }));
});