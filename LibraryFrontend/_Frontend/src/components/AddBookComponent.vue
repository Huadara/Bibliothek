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
    <h1>{{addBook}}</h1>
  </div>
</template>

<script>
  import axios from 'axios';

  export default {
    name: "AddBookComponent",
    data() {
      return {
        title: "",
        author: "",
        publisher: "",
        isbn: "",
        price: ""
      }
    },
    methods: {
      addBook() {
        const json = { title: '${title}', author: '${author}', publisher: '${publisher}', isbn: '${isbn}', price: '${price}'};
        axios.post('http://localhost:5000/library/books',
          {
            body: {
              title: 'a',
              author: 'b',
              publisher: 'c',
              isbn: 'd',
              price: 'e'
            }
          });
        return json;
      }
      ,
      testget() {
        return axios.get('http://localhost:5000/library/books/1',
          {
              headers:{
                'Content-type': 'application/json'
              }
          })
          .then(function (response) {

            console.log(response.data);
          })
          .catch(function (error) {

          });
      }
    },
  }
</script>

<style scoped>

</style>

