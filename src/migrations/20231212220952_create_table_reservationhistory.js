exports.up = (knex) => {
  return knex.schema.createTable('reservationhistory', (t) => {
    t.increments('id').primary();
    t.date('reservationdate').notNull();
    t.time('reservationtime').notNull();
    t.string('client_name', 50).notNull();
    t.string('restaurant_name', 50).notNull();
    t.integer('user_id')
      .references('id')
      .inTable('users').notNull();
    t.integer('restaurant_id')
      .references('id')
      .inTable('restaurants').notNull();
  });
};

exports.down = (knex) => {
  return knex.schema.dropTable('reservationhistory');
};
