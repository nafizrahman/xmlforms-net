<?xml version="1.0" encoding="UTF-8" ?>

<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns="http://www.w3.org/1999/xhtml"
  xmlns:h="http://www.w3.org/1999/xhtml"
  xmlns:r="http://www.rg.com"
  exclude-result-prefixes="h">

    
  <xsl:output method="html" indent="yes" encoding="utf-8"/>
    
    <!-- kopia nodów, chyba że inny szablon się zmatchuje ... -->
    <xsl:template match="@*|node()">
      <xsl:copy>
        <xsl:apply-templates select="@*|node()"/>
      </xsl:copy>
    </xsl:template>
    
    <xsl:template match="r:panel">
        <div style="border:solid 3px blue;background:yellow;scroll:auto;" class="cp_panel">
            <xsl:attribute name="id"><xsl:value-of select="@id" /></xsl:attribute>
            <script language="javascript">
                $('#<xsl:value-of select="@id" />').attr('source', '<xsl:value-of select="@href" />');
                $(document).ready(function(){ 
                    var href = $('#<xsl:value-of select="@id" />').attr('source');
                    $('#<xsl:value-of select="@id" />').load(href);
                });
                 
            </script>
        </div>
    </xsl:template>
    <!-- a tu bierzemy nody z namespacu r: - zamykamy w span i przetwarzamy reszte-->
    <xsl:template match="r:span">
        <span style="background:yellow;font-weight:bold">
            <xsl:apply-templates select="node()" />
        </span>
    </xsl:template>
    
    <xsl:template match="r:webpage">
        <html>
            <head>
                <script src="jquery-1.1.3.1.js"></script>
                <script src="json2.js"></script>
				<script src="trpc.js"></script>
				<script src="carpatia.js"></script>
            </head>
            <body style="background:green">
                <xsl:apply-templates select="node()" />
            </body>
        </html>
    </xsl:template>
    
    <xsl:template match="r:webform">
        <span class="cp_webform">
			<xsl:attribute name="id"><xsl:value-of select="@id" /></xsl:attribute>
			<xsl:attribute name="name"><xsl:value-of select="@name" /></xsl:attribute>
			<xsl:apply-templates select="node()" />
		</span>
    </xsl:template>
    
    <!-- r:context atrybuty objref -->
    <xsl:template match="r:context">
		<form class="cp_objcontext">
            <xsl:attribute name="action">form_postback.aspx</xsl:attribute>
            <xsl:attribute name="id"><xsl:value-of select="@id" /></xsl:attribute>
            <input type="hidden" name="__tid"></input>
            <input type="hidden" name="__form_objref"><!-- referencja do obiektu -->
                <xsl:attribute name="value"><xsl:value-of select="@objref" /></xsl:attribute>
            </input>
            <xsl:apply-templates select="node()" />
			<br />
            <button name="Clear" type="button"><xsl:attribute name="onclick">CP_ClearFormFields('<xsl:value-of select="@id" />')</xsl:attribute>Clear</button>
			<button name="Save" type="button"><xsl:attribute name="onclick">CP_SaveForm('<xsl:value-of select="@id" />')</xsl:attribute>Save</button>
        </form>
        <script language="javascript">
            $(document).ready(function(){ 
				$('#<xsl:value-of select="@id" />').submit(function() {
                    var inputs = [];
                    var t = "";
                    $(':input', this).each(function() {
                        inputs[this.name] = this.value;
                        t = t + "\n" + this.name + '=' + this.value;
                    })
                    alert("inputs: " + t);
                    return false;
                });
                
            });
        </script>
    </xsl:template>
    
    <xsl:template match="r:field">
        <xsl:choose>
            <xsl:when test="@mode='Text'">
                <xsl:apply-templates select="." mode="Text" />
            </xsl:when>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="r:field" mode="Text">
        <span class="qq_field">
            <span class="qq_field_label"><xsl:value-of select="@name" /></span>
            <br/>
            <input class="cp_data_field">
                <xsl:attribute name="name"><xsl:value-of select="@name" /></xsl:attribute>
                <xsl:attribute name="type">text</xsl:attribute>
                <xsl:attribute name="value"><xsl:value-of select="@value" /></xsl:attribute>
                <xsl:attribute name="id"><xsl:value-of select="@id" /></xsl:attribute>
            </input>
        </span>
    </xsl:template>
	
</xsl:stylesheet>
    