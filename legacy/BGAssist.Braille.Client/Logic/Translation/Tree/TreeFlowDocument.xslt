<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:bgabclds="clr-namespace:BGAssist.Braille.Client.Logic.DocumentStructure;assembly=BGAssist.Braille.Client" 
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl x">

  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>

  <xsl:template match="x:Section[not(parent::x:Section)]">
    <Section>
      <xsl:apply-templates select="node()"/>
    </Section>
  </xsl:template>
  

  <xsl:template match="x:Section">
    <Section>
    <xsl:apply-templates select="node()"/>
    </Section>
  </xsl:template>

  <xsl:template match="x:Volume">
    <Document>
      <Volume>
      <Section>
        <Paragraph>
      <xsl:apply-templates select="node()"/>
          </Paragraph>
        </Section>
    </Volume>
      </Document>
  </xsl:template>

  <xsl:template match="x:Paragraph">
    <Paragraph>
      <xsl:apply-templates select="node()"/>
    </Paragraph>
  </xsl:template>

  <xsl:template match="x:TextRun">
    <Run>
      <xsl:apply-templates select="node()"/>
    </Run>
  </xsl:template>

  <xsl:template match="bgabclds:Volume">
    <Document>
      <Volume>
        <xsl:apply-templates select="node()"/>
      </Volume>
    </Document>
  </xsl:template>

</xsl:stylesheet>