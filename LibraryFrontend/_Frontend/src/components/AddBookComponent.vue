<template>
    <div>
      <h1>BOOK</h1>
      <b-container>
        <b-row>
          <b-col>
            <h4>Title:</h4>
          </b-col>
          <b-col>
            <input v-model="title">
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Author:</h4>
          </b-col>
          <b-col>
            <input v-model="author">
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Publisher:</h4>
          </b-col>
          <b-col>
            <input v-model="publisher">
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Supplier:</h4>
          </b-col>
          <b-col>
            <b-dropdown v-model="supplier_id"></b-dropdown>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>ISBN:</h4>
          </b-col>
          <b-col>
            <input v-model="isbn">
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Price:</h4>
          </b-col>
          <b-col>
            <input v-model="price">
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-button :variant="primary" @click="addBook">Add Book</b-button>
          </b-col>
        </b-row>
      </b-container>
      <h1>{{addBook(title,author,publisher,1,isbn,price)}}</h1>
    </div>
</template>

<script>

    export default {
        name: "AddBookComponent",
      data(){
          return{
            title: "",
            author: "",
            publisher: "",
            supplier_id: Number,
            isbn: "",
            price: ""
          }
      },
      methods:{
        addBook(title, author, publisher, supplier_id, isbn, price)
        {
            const axios = require('axios');
            const json = `{ "title": "${title}", "author": "${author}", "publisher": "${publisher}", "supplier_id": ${supplier_id}, "isbn": "${isbn}", "price": ${price}}`
            axios.post("/library/books", json,
              {headers: {
                'Content-type': 'application/json'
                }});
        }
      }
    }
</script>

<style scoped>

</style>

