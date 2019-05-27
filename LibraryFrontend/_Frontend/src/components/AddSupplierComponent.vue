<template>
    <div>
      <h1>SUPPLIER</h1>
      <b-container>
        <b-row>
          <b-col>
            <h4>Company Name:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Address:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-button :variant="primary" onclick="btnAddSupplierClicked">Add Supplier</b-button>
          </b-col>
        </b-row>
      </b-container>
    </div>
</template>

<script>

export default {
        name: "AddSupplierComponent"
    }
</script>

<style scoped>

</style>
