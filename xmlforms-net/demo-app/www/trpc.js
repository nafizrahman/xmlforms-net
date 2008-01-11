///tid - id transakcji
///mode: commit, savepoint, discard
function TRPC(tid, mode)
{
	this.tid = tid;
	this.mode = mode;
	this.operations = new Array();
	this.url = "trpc.aspx";
	
	this.addSetField = function(objref, fldName, val) {
		this.operations[this.operations.length] = new TRPCSetField(objref, fldName, val);
	}
	
	this.addGetField = function(objref, expr) {
		this.operations[this.operations.length] = new TRPCGetField(objref, expr);
	}
	
	
	this.execute = function() {
		var orq = new Object();
		orq.tid = this.tid;
		orq.mode = mode;
		orq.operations = this.operations;
		
		this.results = new Array();
		var s = JSON.stringify(orq);
		alert(s);
		jQuery.post(this.url, s, function(data) {
			try
			{
				alert("response: " + data);
				//var orsp = JSON.parse(data);
				var orsp = eval('(' + data + ')');;
				alert("resp tid: " + orsp.tid);
			}
			catch(e)
			{
				alert("error:" + JSON.stringify(e));
			}
		});
	}
	
}

function TRPCSetField(objref, fldName, val)
{
	this.action = "SetField";
	this.name = fldName;
	this.value = val;
	this.ref = objref;
}

function TRPCCall(objref, expr)
{
	this.action = "GetValue";
	this.expr = expr;
	this.ref = objref;
}


function TRPCGetField(objref, expr)
{
	this.action = "GetField";
	this.expr = expr;
	this.ref = objref;
}