const request = require('supertest');
const jwt = require('jwt-simple');

const app = require('../../src/app');

const route = '/restaurants';

const secret = 'ipca!DWM@202324';

let restaurant;
let restaurantResponsible;
let responsibleRegistration;

beforeAll(async () => {
  const registrationRes = await app.services.restaurantregistration.save({
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
  restaurant = { ...registrationRes[0] };

  const registrationResponsible = await app.services.restaurantresponsible.save({
    flname: 'Rui Barreto',
    phone: 912345678,
    email: 'ruibarreto@gmail.com',
    password: '12345',
    restaurantregistration_id: restaurant.id,
  });
  restaurantResponsible = { ...registrationResponsible[0] };

  const res = await app.services.restaurantresponsible.save({
    flname: 'Rui Barreto',
    phone: 912345678,
    email: 'ruibarreto@gmail.com',
    password: '12345',
    image: null,
    restaurantregistration_id: restaurant.id,
  });

  responsibleRegistration = { ...res[0] };
  responsibleRegistration.token = jwt.encode(responsibleRegistration, secret);
});

test('Test #30 - Listar todos os restaurantes', () => {
  return request(app).get(route)
    .then((res) => {
      expect(res.status).toBe(200);
    });
});

test('Test #31 - Listar um restaurante por ID', () => {
  return app.db('restaurants')
    .insert({
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
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    }, ['id'])
    .then((getRestaurant) => request(app).get(`${route}/${getRestaurant[0].id}`)
      .set('Authorization', `bearer ${responsibleRegistration.token}`))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.name).toBe('La Gusto Italiano');
    });
});

test('Test #32 - Inserir um restaurante', () => {
  return request(app).post(route)
    .send({
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
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(201);
      expect(res.body.name).toBe('La Gusto Italiano');
    });
});

test('Test #33 - Inserir um restaurante sem nome do restaurante', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
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
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Nome do restaurante é um atributo obrigatório!');
    });
});

test('Test #34 - Inserir um restaurante sem categoria do restaurante', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
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
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Categoria do restaurante é um atributo obrigatório!');
    });
});

test('Test #35 - Inserir um restaurante sem descrição do restaurante', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      averageprice: 18.75,
      openinghours: '10:30',
      closinghours: '23:00',
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Descrição do restaurante é um atributo obrigatório!');
    });
});

test('Test #36 - Inserir um restaurante sem telefone do restaurante', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      averageprice: 18.75,
      openinghours: '10:30',
      closinghours: '23:00',
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Telefone do restaurante é um atributo obrigatório!');
    });
});

test('Test #37 - Inserir um restaurante sem localização do restaurante', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      averageprice: 18.75,
      openinghours: '10:30',
      closinghours: '23:00',
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Localização do restaurante é um atributo obrigatório!');
    });
});

test('Test #38 - Inserir um restaurante sem imagem do restaurante', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      averageprice: 18.75,
      openinghours: '10:30',
      closinghours: '23:00',
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Imagem do restaurante é um atributo obrigatório!');
    });
});

test('Test #39 - Inserir um restaurante sem número de mesas do restaurante', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      averageprice: 18.75,
      openinghours: '10:30',
      closinghours: '23:00',
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Número de mesas do restaurante é um atributo obrigatório!');
    });
});

test('Test #40 - Inserir um restaurante sem capacidade de pessoas', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      openingdays: 'Aberto de segunda a sábado',
      averageprice: 18.75,
      openinghours: '10:30',
      closinghours: '23:00',
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Capacidade do restaurante é um atributo obrigatório!');
    });
});

test('Test #41 - Inserir um restaurante sem horário de funcionamento', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      averageprice: 18.75,
      openinghours: '10:30',
      closinghours: '23:00',
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Dias de funcionamento são um atributo obrigatório!');
    });
});

test('Test #42 - Inserir um restaurante sem preço médio', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Preço médio é um atributo obrigatório!');
    });
});

test('Test #42 - Inserir um restaurante sem horas de abertura', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
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
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Horas de abertura são um atributo obrigatório!');
    });
});

test('Test #43 - Inserir um restaurante sem horas de fecho', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
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
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Horas de fecho são um atributo obrigatório!');
    });
});

test('Test #44 - Atualizar dados de um restaurante', () => {
  return app.db('restaurants')
    .insert({
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
      restaurantregistration_id: restaurant.id,
      rresponsible_id: restaurantResponsible.id,
    }, ['id'])
    .then((restaurantRes) => request(app).put(`${route}/${restaurantRes[0].id}`)
      .set('Authorization', `bearer ${responsibleRegistration.token}`)
      .send({
        name: 'Picanha Delight Grill',
        category: 'Comida de Picanha',
        desc: 'Restaurante de picanha situado em Braga',
        rphone: 253456789,
        location: 'Rua Gonçalo Sousa 285',
        image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
        numberoftables: 10,
        capacity: 200,
        openingdays: 'Aberto de segunda a sábado',
        averageprice: 28.75,
        openinghours: '11:30',
        closinghours: '23:30',
        restaurantregistration_id: restaurant.id,
        rresponsible_id: restaurantResponsible.id,
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.name).toBe('Picanha Delight Grill');
      expect(res.body.category).toBe('Comida de Picanha');
    });
});
