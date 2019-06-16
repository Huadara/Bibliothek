<template>
  <div>
    <h1>BOOK</h1>
    <b-container>
      <b-row>
        <b-col>
          <h4>Title:</h4>
        </b-col>
        <b-col>
          <input v-model="title" ref="myTitle">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Author:</h4>
        </b-col>
        <b-col>
          <input v-model="author" ref="myAuthor">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Publisher:</h4>
        </b-col>
        <b-col>
          <input v-model="publisher" ref="myPublisher">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>ISBN:</h4>
        </b-col>
        <b-col>
          <input v-model="isbn" ref="myIsbn">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Price:</h4>
        </b-col>
        <b-col>
          <input v-model="price" ref="myPrice">
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
        axios.post('http://localhost:5000/library/books', {
                title: this.$refs.myTitle.value,
                author: this.$refs.myAuthor.value,
                publisher: this.$refs.myPublisher.value,
                isbn: this.$refs.myIsbn.value,
                price: 1.0 //this.$refs.myPrice.value
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

