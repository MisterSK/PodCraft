// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var proto = location.protocol
var host =  location.hostname
var port =  location.port
var uri = proto+'//'+host+(port ? ':'+location.port: '')+'/api/products';

let products = null;
function getCount(data) {
  const el = $("#counter");
  let lender = "product";
  if (data) {
    if (data > 1) {
      lender = "products";
    }
    el.text(data + " " + lender);
  } else {
    el.text("No " + lender);
  }
}

$(document).ready(function() {
  getData();
});

function getData() {
  $.ajax({
    type: "GET",
    url: uri,
    cache: false,
    success: function(data) {
      const tBody = $("#products");

      $(tBody).empty();

      getCount(data.length);

      $.each(data, function(key, lender) {
        const tr = $("<tr></tr>")
          .append($("<td></td>").text(lender.lender))
          .append($("<td></td>").text(lender.interestRate))
          .append($("<td></td>").text(lender.rateType))
          .append($("<td></td>").text(lender.ltvRatio))

        tr.appendTo(tBody);
      });

      products = data;
    }
  });
}