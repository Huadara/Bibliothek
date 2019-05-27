<template>
    <div>
      <h1>BOOK</h1>
      <b-container>
        <b-row>
          <b-col>
            <h4>Title:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Author:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Publisher:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Supplier:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>ISBN:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Price:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-button :variant="primary">Add Book</b-button>
          </b-col>
        </b-row>
      </b-container>
    </div>
</template>

<script>
    export default {
        name: "AddBookComponent"
    }
</script>

<style scoped>

</style>
