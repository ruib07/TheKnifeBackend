/* eslint-disable no-unused-vars */
const request = require('supertest');

const app = require('../../src/app');

const route = '/comments';

let userRes;
let user;
let restaurantRes;
let responsible;
let restaurant;

beforeAll(async () => {
  const registerUser = await app.services.registeruser.save({
    username: 'goncalosousa',
    email: 'goncalosousa@gmail.com',
    password: 'goncalo123',
  });
  userRes = { ...registerUser[0] };

  const userTable = await app.services.user.save({
    username: 'goncalosousa',
    email: 'goncalosousa@gmail.com',
    password: 'goncalo123',
    image: null,
    registeruser_id: userRes.id,
  });

  user = { ...userTable };

  const restaurantRegistration = await app.services.restaurantregistration.save({
    flname: 'Rui Barreto',
    phone: 912345678,
    email: 'ruibarreto@gmail.com',
    password: '12345',
    name: 'La Gusto Italiano',
    category: 'Comida Italiana',
    desc: 'Restaurante de comida italiana situado em Braga',
    rphone: 253456789,
    location: 'Rua Gonçalo Sousa 285',
    image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
    numberoftables: 10,
    capacity: 200,
    openingdays: 'Aberto de segunda a sábado',
    averageprice: 18.75,
    openinghours: '10:30',
    closinghours: '23:00',
  });

  restaurantRes = { ...restaurantRegistration[0] };

  const responsibleRes = await app.services.restaurantresponsible.save({
    flname: 'Rui Barreto',
    phone: 912345678,
    email: 'ruibarreto@gmail.com',
    password: '12345',
    restaurantregistration_id: restaurantRes.id,
  });

  responsible = { ...responsibleRes[0] };

  const restaurantTable = await app.services.restaurant.save({
    name: 'La Gusto Italiano',
    category: 'Comida Italiana',
    desc: 'Restaurante de comida italiana situado em Braga',
    rphone: 253456789,
    location: 'Rua Gonçalo Sousa 285',
    image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
    numberoftables: 10,
    capacity: 200,
    openingdays: 'Aberto de segunda a sábado',
    averageprice: 18.75,
    openinghours: '10:30',
    closinghours: '23:00',
    restaurantregistration_id: restaurantRes.id,
    rresponsible_id: responsible.id,
  });

  restaurant = { ...restaurantTable };
});

test('Test #80 - Listar todos os comentários', () => {
  return request(app).get(route)
    .then((res) => {
      expect(res.status).toBe(200);
    });
});

test('Test #81 - Listar um comentário por ID', () => {
  return app.db('comments').insert({
    username: 'goncalosousa',
    commentdate: '10-01-2024',
    review: 10,
    comment: 'Restaurante com comida excelente!',
    user_id: user.id,
    restaurant_id: restaurant.id,
  }, ['id'])
    .then((getComment) => request(app).get(`${route}/${getComment[0].id}`))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.username).toBe('goncalosousa');
    });
});

test('Test #82 - Inserir um comentário', () => {
  return request(app).post(route)
    .send({
      username: 'goncalosousa',
      commentdate: '10-01-2024',
      review: 10,
      comment: 'Restaurante com comida excelente!',
      user_id: user.id,
      restaurant_id: restaurant.id,
    })
    .then((res) => {
      expect(res.status).toBe(201);
    });
});

test('Test #83 - Inserir um comentário sem username', () => {
  return request(app).post(route)
    .send({
      commentdate: '10-01-2024',
      review: 10,
      comment: 'Restaurante com comida excelente!',
      user_id: user.id,
      restaurant_id: restaurant.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Username é um atributo obrigatório!');
    });
});

test('Test #84 - Inserir um comentário sem data de comentário', () => {
  return request(app).post(route)
    .send({
      username: 'goncalosousa',
      review: 10,
      comment: 'Restaurante com comida excelente!',
      user_id: user.id,
      restaurant_id: restaurant.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Data do comentário é um atributo obrigatório!');
    });
});

test('Test #85 - Inserir um comentário sem review', () => {
  return request(app).post(route)
    .send({
      username: 'goncalosousa',
      commentdate: '10-01-2024',
      comment: 'Restaurante com comida excelente!',
      user_id: user.id,
      restaurant_id: restaurant.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Review é um atributo obrigatório!');
    });
});

test('Test #86 - Inserir um comentário sem comentário', () => {
  return request(app).post(route)
    .send({
      username: 'goncalosousa',
      commentdate: '10-01-2024',
      review: 10,
      user_id: user.id,
      restaurant_id: restaurant.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Comentário é um atributo obrigatório!');
    });
});

test('Test #87 - Atualizar dados de um comentário', () => {
  return app.db('comments')
    .insert({
      username: 'goncalosousa',
      commentdate: '10-01-2024',
      review: 10,
      comment: 'Restaurante com comida excelente!',
      user_id: user.id,
      restaurant_id: restaurant.id,
    }, ['id'])
    .then((commentRes) => request(app).put(`${route}/${commentRes[0].id}`)
      .send({
        username: 'ruibarreto',
        commentdate: '10-01-2024',
        review: 8,
        comment: 'Restaurante com comida excelente mas um bocado caro!',
        user_id: user.id,
        restaurant_id: restaurant.id,
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.username).toBe('ruibarreto');
    });
});

test('Test #88 - Remover um comentário', () => {
  return app.db('comments')
    .insert({
      username: 'goncalosousa',
      commentdate: '10-01-2024',
      review: 10,
      comment: 'Restaurante com comida excelente!',
      user_id: user.id,
      restaurant_id: restaurant.id,
    }, ['id'])
    .then((commentRes) => request(app).delete(`${route}/${commentRes[0].id}`)
      .send({
        username: 'Comentário Removido',
      }))
    .then((res) => {
      expect(res.status).toBe(204);
    });
});
