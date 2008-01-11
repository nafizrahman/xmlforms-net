function ObjectContext(ctxName)
{
	this.name = ctxName;
	
}


//wyczyszczenie pol w formularzu o podanym ID
function CP_ClearFormFields(formid)
{
	$('#' + formid).each(function() {
		$(':input.cp_data_field', this).each(function() {
			this.value = "";
		});
	});
}

//powoduje ponowne wczytanie panelu z podanym ID
function CP_ReloadPanel(panel_id)
{
	var p = $('#' + panel_id);
	var href = p.attr('source');
	p.load(href);
}

//wypelnia postback danymi z formularza
//ale nie wysyla postbacku
function CP_PrepareFormRCPPostback(formid, trpc)
{
	var f = $('#' + formid).eq(0);
	//alert("L:" + f);
	var objref = $("#" + formid + " input[@name='__form_objref']").val();
	//alert("objref:" + objref);
	$(':input.cp_data_field', f).each(function() {
		trpc.addSetField(objref, this.name, this.value);
	});
}

function CP_SaveForm(formid) {
	var f = $('#' + formid);
	var rpc = new TRPC("", "commit");
	CP_PrepareFormRCPPostback(formid, rpc);
	rpc.execute();
}

function CPValidateFormFields(formid)
{
	
}