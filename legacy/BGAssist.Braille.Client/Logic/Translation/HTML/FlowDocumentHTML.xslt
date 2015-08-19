<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl x">

  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>

  <xsl:template match="x:Section[not(parent::x:Section)]">
    <div>
      <xsl:apply-templates select="node()"/>
    </div>
  </xsl:template>

  <xsl:template match="x:Section">
    <xsl:apply-templates select="node()"/>
  </xsl:template>

  <xsl:template match="x:Paragraph">
    <p>
      <xsl:apply-templates select="node()"/>
    </p>
  </xsl:template>

  <xsl:template match="x:Run">
    <xsl:variable name="class">
      <xsl:if test="@FontStyle='Italic'">
        <xsl:text>i </xsl:text>
      </xsl:if>
      <xsl:if test="@FontWeight='Bold'">
        <xsl:text>b </xsl:text>
      </xsl:if>
      <xsl:if test="contains(@TextDecorations, 'Underline')">
        <xsl:text>u </xsl:text>
      </xsl:if>
    </xsl:variable>
    <span>
      <xsl:if test="normalize-space($class) != ''">
        <xsl:attribute name="class">
          <xsl:value-of select="normalize-space($class)"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:value-of select="text()"/>
    </span>
  </xsl:template>

</xsl:stylesheet>